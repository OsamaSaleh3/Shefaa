using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shefaa.Application.MedicalRecords.Commands.CreateMedicalRecord;
using Shefaa.Application.MedicalRecords.Commands.UpdateMedicalRecordVitals;
using Shefaa.Application.MedicalRecords.Commands.UpdateDiagnosis;
using Shefaa.Application.MedicalRecords.Queries.GetMedicalRecordById;
using Shefaa.Application.MedicalRecords.Queries.GetMedicalRecordByAppointment;
using Shefaa.Application.MedicalRecords.Queries.GetPatientMedicalHistory;
using Shefaa.Contracts.MedicalRecords;

namespace Shefaa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicalRecordsController : ControllerBase
{
    private readonly ISender _sender;

    public MedicalRecordsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMedicalRecord([FromBody] CreateMedicalRecordRequest request)
    {
        var command = new CreateMedicalRecordCommand(
            request.PatientId,
            request.DoctorId,
            request.ChiefComplaint,
            request.Symptoms,
            request.Diagnosed,
            request.BloodPressure,
            request.Temperature,
            request.Pulse,
            request.RespiratoryRate,
            request.Weight,
            request.Height,
            request.AppointmentId
        );

        var result = await _sender.Send(command);

        return result.Match(
            recordId => CreatedAtAction(nameof(GetMedicalRecordById), new { id = recordId }, new { id = recordId }),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/vitals")]
    public async Task<IActionResult> UpdateMedicalRecordVitals(Guid id, [FromBody] UpdateMedicalRecordVitalsRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest("ID mismatch");
        }

        var command = new UpdateMedicalRecordVitalsCommand(
            request.Id,
            request.BloodPressure,
            request.Temperature,
            request.Pulse,
            request.RespiratoryRate,
            request.Weight,
            request.Height
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/diagnosis")]
    public async Task<IActionResult> UpdateDiagnosis(Guid id, [FromBody] UpdateDiagnosisRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest("ID mismatch");
        }

        var command = new UpdateDiagnosisCommand(
            request.Id,
            request.NewDiagnosis,
            request.AdditionalNotes
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMedicalRecordById(Guid id)
    {
        var query = new GetMedicalRecordByIdQuery(id);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            medicalRecordDto => medicalRecordDto is null
                ? NotFound()
                : Ok(new MedicalRecordResponse(
                    medicalRecordDto.Id,
                    medicalRecordDto.PatientId,
                    medicalRecordDto.PatientName,
                    medicalRecordDto.DoctorName,
                    medicalRecordDto.VisitDate,
                    medicalRecordDto.ChiefComplaint,
                    medicalRecordDto.Diagnosis,
                    medicalRecordDto.BloodPressure,
                    medicalRecordDto.Temperature
                )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("appointment/{appointmentId}")]
    public async Task<IActionResult> GetMedicalRecordByAppointment(Guid appointmentId)
    {
        var query = new GetMedicalRecordByAppointmentQuery(appointmentId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            medicalRecordDto => medicalRecordDto is null
                ? NotFound()
                : Ok(new MedicalRecordResponse(
                    medicalRecordDto.Id,
                    medicalRecordDto.PatientId,
                    medicalRecordDto.PatientName,
                    medicalRecordDto.DoctorName,
                    medicalRecordDto.VisitDate,
                    medicalRecordDto.ChiefComplaint,
                    medicalRecordDto.Diagnosis,
                    medicalRecordDto.BloodPressure,
                    medicalRecordDto.Temperature
                )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientMedicalHistory(Guid patientId)
    {
        var query = new GetPatientMedicalHistoryQuery(patientId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            medicalRecords => Ok(medicalRecords.Select(mr => new MedicalRecordResponse(
                mr.Id,
                mr.PatientId,
                mr.PatientName,
                mr.DoctorName,
                mr.VisitDate,
                mr.ChiefComplaint,
                mr.Diagnosis,
                mr.BloodPressure,
                mr.Temperature
            )).ToList()),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    private static int GetStatusCode(List<Error> errors)
    {
        return errors.First().Type switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
