using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shefaa.Application.Prescriptions.Commands.AddMedicationToPrescription;
using Shefaa.Application.Prescriptions.Commands.CreatePrescription;
using Shefaa.Application.Prescriptions.Commands.RemoveMedicationFromPrescription;
using Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionMedication;
using Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionNotes;
using Shefaa.Application.Prescriptions.Dtos;
using Shefaa.Application.Prescriptions.Queries.GetPatientPrescriptionsHistory;
using Shefaa.Application.Prescriptions.Queries.GetPrescriptionById;
using Shefaa.Application.Prescriptions.Queries.GetPrescriptionsByMedicalRecord;
using Shefaa.Contracts.Prescriptions;

namespace Shefaa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PrescriptionsController : ControllerBase
{
    private readonly ISender _sender;

    public PrescriptionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequest request)
    {
        var command = new CreatePrescriptionCommand(
            request.PatientId,
            request.DoctorId,
            request.MedicalRecordId,
            request.Notes
        );

        var result = await _sender.Send(command);

        return result.Match(
            prescriptionId => CreatedAtAction(nameof(GetPrescriptionById), new { id = prescriptionId }, new { id = prescriptionId }),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/medication")]
    public async Task<IActionResult> AddMedicationToPrescription(Guid id, [FromBody] AddMedicationToPrescriptionRequest request)
    {
        var command = new AddMedicationToPrescriptionCommand(
            id,
            request.MedicationName,
            request.Dosage,
            request.Frequency,
            request.Duration,
            request.Instructions
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpDelete("{id}/medication")]
    public async Task<IActionResult> RemoveMedicationFromPrescription(Guid id, [FromBody] RemoveMedicationFromPrescriptionRequest request)
    {
        var command = new RemoveMedicationFromPrescriptionCommand(
            id,
            request.MedicationName
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/medication-details")]
    public async Task<IActionResult> UpdatePrescriptionMedication(Guid id, [FromBody] UpdatePrescriptionMedicationRequest request)
    {
        var command = new UpdatePrescriptionMedicationCommand(
            id,
            request.MedicationName,
            request.NewDosage,
            request.NewFrequency,
            request.NewDuration,
            request.NewInstructions
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/notes")]
    public async Task<IActionResult> UpdatePrescriptionNotes(Guid id, [FromBody] UpdatePrescriptionNotesRequest request)
    {
        var command = new UpdatePrescriptionNotesCommand(
            id,
            request.NewNotes
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPrescriptionById(Guid id)
    {
        var query = new GetPrescriptionByIdQuery(id);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            prescriptionDto => prescriptionDto is null
                ? NotFound()
                : Ok(new PrescriptionResponse(
                    prescriptionDto.Id,
                    prescriptionDto.MedicalRecordId,
                    prescriptionDto.DoctorName,
                    prescriptionDto.PatientName,
                    prescriptionDto.Date,
                    prescriptionDto.Notes,
                    new List<PrescriptionMedicationResponse>(
                        prescriptionDto.Medications.Select(pm => new PrescriptionMedicationResponse(
                            pm.Name,
                            pm.Dosage,
                            pm.Frequency,
                            pm.Duration,
                            pm.Instructions
                            ))
                        )
                )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("medical-record/{medicalRecordId}")]
    public async Task<IActionResult> GetPrescriptionsByMedicalRecord(Guid medicalRecordId)
    {
        var query = new GetPrescriptionsByMedicalRecordQuery(medicalRecordId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            prescriptions => Ok(prescriptions.Select(p => new PrescriptionResponse(
                p.Id,
                p.MedicalRecordId,
                p.DoctorName,
                p.PatientName,
                p.Date,
                p.Notes,
                new List<PrescriptionMedicationResponse>(
                        p.Medications.Select(pm => new PrescriptionMedicationResponse(
                            pm.Name,
                            pm.Dosage,
                            pm.Frequency,
                            pm.Duration,
                            pm.Instructions
                            ))
                        )
            )).ToList()),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientPrescriptionsHistory(Guid patientId)
    {
        var query = new GetPatientPrescriptionsHistoryQuery(patientId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            prescriptions => Ok(prescriptions.Select(p => new PrescriptionResponse(
                p.Id,
                p.MedicalRecordId,
                p.DoctorName,
                p.PatientName,
                p.Date,
                p.Notes,
                new List<PrescriptionMedicationResponse>(
                        p.Medications.Select(pm => new PrescriptionMedicationResponse(
                            pm.Name,
                            pm.Dosage,
                            pm.Frequency,
                            pm.Duration,
                            pm.Instructions
                            ))
                        )
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
