using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;

namespace Shefaa.Application.Prescriptions.Commands.UpdatePrescriptionNotes;

public class UpdatePrescriptionNotesCommandHandler : IRequestHandler<UpdatePrescriptionNotesCommand, ErrorOr<Success>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public UpdatePrescriptionNotesCommandHandler(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }
    public async Task<ErrorOr<Success>> Handle(UpdatePrescriptionNotesCommand request, CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(request.PrescriptionId);
        if (prescription is null)
            return Error.NotFound("Prescription.NotFound", $"Prescription with id {request.PrescriptionId} was not found.");

        prescription.UpdateNotes(request.NewNotes);
        await _prescriptionRepository.UpdateAsync(prescription);
        return Result.Success;
    }
}