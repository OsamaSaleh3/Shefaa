namespace Shefaa.Contracts.Appointments;

public record BookAppointmentRequest(
    Guid PatientId,
    string DoctorId,
    DateTime AppointmentDate,
    int DurationMinutes,
    string? Notes
);
