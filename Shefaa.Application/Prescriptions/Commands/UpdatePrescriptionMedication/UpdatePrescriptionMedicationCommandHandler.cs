using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionMedication;

public class UpdatePrescriptionMedicationCommandHandler : IRequestHandler<UpdatePrescriptionMedicationCommand, ErrorOr<Success>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public UpdatePrescriptionMedicationCommandHandler(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }
    public async Task<ErrorOr<Success>> Handle(UpdatePrescriptionMedicationCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetByIdWithMedicationsAsync(request.PrescriptionId);
        if (prescription is null)
            return Error.NotFound("Prescription.NotFound", $"Prescription with id {request.PrescriptionId} was not found.");

        var UpdateMedicationResult = prescription.UpdateMedicationDetails(request.MedicationName,
                                                                          request.NewDosage,
                                                                          request.NewFrequency,
                                                                          request.NewDuration,
                                                                          request.NewInstructions);
        if(UpdateMedicationResult.IsError)
            return UpdateMedicationResult.Errors;

        await _prescriptionRepository.UpdateAsync(prescription);
        return Result.Success;
    }
}