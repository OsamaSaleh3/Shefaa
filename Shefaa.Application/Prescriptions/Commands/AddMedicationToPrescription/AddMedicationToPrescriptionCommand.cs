using ErrorOr;
using MediatR;

namespace Shefaa.Application.Prescriptions.Commands.AddMedicationToPrescription;

public record AddMedicationToPrescriptionCommand(
    Guid PrescriptionId,
    string MedicationName,
    string Dosage,    
    string Frequency, 
    string Duration,  
    string? Instructions
) : IRequest<ErrorOr<Success>>;