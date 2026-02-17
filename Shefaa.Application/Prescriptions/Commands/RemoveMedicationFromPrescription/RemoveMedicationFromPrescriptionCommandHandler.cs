using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Prescriptions.Commands.RemoveMedicationFromPrescription;

public class RemoveMedicationFromPrescriptionCommandHandler : IRequestHandler<RemoveMedicationFromPrescriptionCommand, ErrorOr<Success>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public RemoveMedicationFromPrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }
    public async Task<ErrorOr<Success>> Handle(RemoveMedicationFromPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetByIdWithMedicationsAsync(request.PrescriptionId);

        if (prescription is null)
            return Error.NotFound("Prescription.NotFound", $"Prescription with id {request.PrescriptionId} was not found.");

        var RemoveMedicationResult = prescription.RemoveMedication(request.MedicationName);
        if (RemoveMedicationResult.IsError)
        {
            return RemoveMedicationResult.Errors;
        }

        await _prescriptionRepository.UpdateAsync(prescription);
        return Result.Success;
    }
}