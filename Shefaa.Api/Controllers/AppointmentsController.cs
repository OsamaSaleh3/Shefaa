using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shefaa.Application.Appointments.Commands.BookAppointment;
using Shefaa.Application.Appointments.Commands.CancelAppointment;
using Shefaa.Application.Appointments.Commands.CompleteAppointment;
using Shefaa.Application.Appointments.Commands.RescheduleAppointment;
using Shefaa.Application.Appointments.Queries.GetAppointmentById;
using Shefaa.Application.Appointments.Queries.GetCalendarView;
using Shefaa.Application.Appointments.Queries.GetDoctorAppointments;
using Shefaa.Application.Appointments.Queries.GetPatientAppointments;
using Shefaa.Contracts.Appointments;

namespace Shefaa.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly ISender _sender;

    public AppointmentsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest request)
    {
        var command = new BookAppointmentCommand(
            request.PatientId,
            request.DoctorId,
            request.AppointmentDate,
            request.DurationMinutes,
            request.Notes
        );

        var result = await _sender.Send(command);

        return result.Match(
            appointmentId => CreatedAtAction(nameof(GetAppointment), new { id = appointmentId }, new { id = appointmentId }),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/reschedule")]
    public async Task<IActionResult> RescheduleAppointment(Guid id, [FromBody] RescheduleAppointmentRequest request)
    {
        var command = new RescheduleAppointmentCommand(
            id,
            request.NewDate           
        );

        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/cancel")]
    public async Task<IActionResult> CancelAppointment(Guid id, [FromBody] CancelAppointmentRequest request)
    {
   
        var command = new CancelAppointmentCommand(id, request.Reason);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteAppointment(Guid id)
    {
        var command = new CompleteAppointmentCommand(id);
        var result = await _sender.Send(command);

        return result.Match<IActionResult>(
            _ => NoContent(),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var query = new GetAppointmentByIdQuery(id);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            appointmentDto => appointmentDto is null
                ? NotFound()
                : Ok(new AppointmentResponse(
                    appointmentDto.Id,
                    appointmentDto.PatientId,
                    appointmentDto.PatientName,
                    appointmentDto.DoctorId,
                    appointmentDto.DoctorName,
                    appointmentDto.Specialization,
                    appointmentDto.AppointmentDate,
                    appointmentDto.Status,
                    appointmentDto.DurationMinutes,
                    appointmentDto.Notes
                )),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("calendar")]
    public async Task<IActionResult> GetCalendarView([FromQuery] string? doctorId, [FromQuery] DateTime from, [FromQuery] DateTime to)
    {
        var query = new GetCalendarViewQuery(from, to, doctorId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            calendarItems => Ok(calendarItems.Select(item => new CalendarItemResponse(
                item.Id,
                item.Title,
                item.Start,
                item.End,
                item.Status
            )).ToList()),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("doctor/{doctorId}")]
    public async Task<IActionResult> GetDoctorAppointments(string doctorId, [FromQuery] DateTime date)
    {
        var query = new GetDoctorAppointmentsQuery(doctorId, date);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            appointments => Ok(appointments.Select(apt => new DoctorAppointmentResponse(
                apt.Id,
                apt.PatientName,
                apt.PatientAge,
                apt.Gender,
                apt.Status,
                apt.Notes,
                apt.Time
            )).ToList()),
            errors => Problem(statusCode: GetStatusCode(errors), detail: errors.First().Description)
        );
    }

    [HttpGet("patient/{patientId}")]
    public async Task<IActionResult> GetPatientAppointments(Guid patientId)
    {
        var query = new GetPatientAppointmentsQuery(patientId);
        var result = await _sender.Send(query);

        return result.Match<IActionResult>(
            appointments => Ok(appointments.Select(apt => new PatientAppointmentResponse(
                apt.Id,
                apt.DoctorName,
                apt.Specialization,
                apt.AppointmentDate,
                apt.Time,
                apt.Status
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
