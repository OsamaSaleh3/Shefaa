namespace Shefaa.Contracts.Appointments;

public record DoctorAppointmentResponse(
    Guid Id,
    string PatientName,
    int PatientAge,
    string Gender,
    string Status,
    string? Notes,
    DateTime Time
);
