namespace Shefaa.Contracts.Appointments;

public record AppointmentResponse(
    Guid Id,
    Guid PatientId,
    string PatientName,
    string DoctorId,
    string DoctorName,
    string Specialization,
    DateTime AppointmentDate,
    string Status,
    int DurationMinutes,
    string? Notes
);
