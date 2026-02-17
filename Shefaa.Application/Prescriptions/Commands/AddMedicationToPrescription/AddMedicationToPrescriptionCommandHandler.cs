using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Prescriptions.Commands.AddMedicationToPrescription;

public class AddMedicationToPrescriptionCommandHandler:IRequestHandler<AddMedicationToPrescriptionCommand, ErrorOr<Success>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public AddMedicationToPrescriptionCommandHandler(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AddMedicationToPrescriptionCommand request, CancellationToken cancellationToken)
    {
        var prescription =await _prescriptionRepository.GetByIdWithMedicationsAsync(request.PrescriptionId);
        if(prescription is null)
            return Error.NotFound("Prescription.NotFound", $"Prescription with id {request.PrescriptionId} was not found.");

        var AddMedicationResult=prescription.AddMedication(request.MedicationName,
                                                     request.Dosage,
                                                     request.Frequency,
                                                     request.Duration,
                                                     request.Instructions);
        if (AddMedicationResult.IsError)
        {
            return AddMedicationResult.Errors;
        }
        await _prescriptionRepository.UpdateAsync(prescription);
        return Result.Success;

    }
}
