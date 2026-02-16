namespace Shefaa.Contracts.Appointments;

public record PatientAppointmentResponse(
    Guid Id,
    string DoctorName,
    string Specialization,
    DateTime AppointmentDate,
    string Time,
    string Status
);
