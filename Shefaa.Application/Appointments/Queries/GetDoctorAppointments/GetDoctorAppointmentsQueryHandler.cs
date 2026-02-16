using ErrorOr;
using MediatR;
using Shefaa.Application.Appointments.Dtos;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Appointments.Queries.GetDoctorAppointments;

public class GetDoctorAppointmentsQueryHandler : IRequestHandler<GetDoctorAppointmentsQuery, ErrorOr<List<DoctorAppointmentDto>>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetDoctorAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }

    public async Task<ErrorOr<List<DoctorAppointmentDto>>> Handle(GetDoctorAppointmentsQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetDoctorAppointmentsAsync(request.DoctorId, request.Date);

        return appointments.Select(a => new DoctorAppointmentDto
        (
            a.Id,
            $"{a.Patient.FirstName} {a.Patient.LastName}",
            a.Patient.GetAge(),
            a.Patient.Gender.ToString(),
            a.Status.ToString(),
            a.Notes,
            a.AppointmentDate
        )).ToList();
    }
}
