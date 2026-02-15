using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shefaa.Application.Patients.Commands.CreatePatient;
using Shefaa.Application.Patients.Commands.DeletePatient;
using Shefaa.Application.Patients.Commands.UpdatePatient;
using Shefaa.Application.Patients.Queries.GetPatientById;
using Shefaa.Application.Patients.Queries.GetPatients;
using Shefaa.Contracts.Patients;

namespace Shefaa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PatientsController : ControllerBase
{
    private readonly ISender _sender;

    public PatientsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePatient([FromBody] CreatePatientRequest request)
    {
        var command = new CreatePatientCommand(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.PhoneNumber,
            request.Address,
            request.EmergencyContactName,
            request.EmergencyContactPhone,
            request.Email,
            request.BloodType,
            request.GeneralNotes
        );

        var result = await _sender.Send(command);

        return result.Match(
            patientId => CreatedAtAction(nameof(GetPatient), new { id = patientId }, new { id = patientId }),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] UpdatePatientRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest("ID mismatch");
        }

        var command = new UpdatePatientCommand(
            request.Id,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Gender,
            request.Phone,
            request.Address,
            request.Email,
            request.EmergencyContactName,
            request.EmergencyContactPhone,
            request.BloodType,
            request.GeneralNotes
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(Guid id)
    {
        var command = new DeletePatientCommand(id);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatient(Guid id)
    {
        var query = new GetPatientByIdQuery(id);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            patientDto => patientDto is null
                ? NotFound()
                : Ok(new PatientResponse(
                    patientDto.Id,
                    patientDto.FileNumber,
                    patientDto.FirstName,
                    patientDto.LastName,
                    patientDto.FullName,
                    patientDto.DateOfBirth,
                    patientDto.Age,
                    patientDto.Gender,
                    patientDto.Phone,
                    patientDto.Email,
                    patientDto.Address,
                    patientDto.BloodType,
                    patientDto.EmergencyContactName,
                    patientDto.EmergencyContactPhone,
                    patientDto.GeneralNotes
                )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPatients()
    {
        var query = new GetPatientsQuery();
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            patientDtos => Ok(patientDtos.Select(patientDto => new PatientResponse(
                patientDto.Id,
                patientDto.FileNumber,
                patientDto.FirstName,
                patientDto.LastName,
                patientDto.FullName,
                patientDto.DateOfBirth,
                patientDto.Age,
                patientDto.Gender,
                patientDto.Phone,
                patientDto.Email,
                patientDto.Address,
                patientDto.BloodType,
                patientDto.EmergencyContactName,
                patientDto.EmergencyContactPhone,
                patientDto.GeneralNotes
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
