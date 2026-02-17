namespace Shefaa.Contracts.Prescriptions;

public record UpdatePrescriptionNotesRequest(
    Guid PrescriptionId,
    string? NewNotes
);
