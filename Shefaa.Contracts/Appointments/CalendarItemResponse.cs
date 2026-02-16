namespace Shefaa.Contracts.Appointments;

public record CalendarItemResponse(
    Guid Id,
    string Title,
    DateTime Start,
    DateTime End,
    string Status
);
