using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Prescriptions.Dtos;

namespace Shefaa.Application.Prescriptions.Queries.GetPrescriptionById;

public class GetPrescriptionByIdQueryHandler : IRequestHandler<GetPrescriptionByIdQuery, ErrorOr<PrescriptionDto>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetPrescriptionByIdQueryHandler(IPrescriptionRepository prescriptionRepository)
    {
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<ErrorOr<PrescriptionDto>> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
    {
        var prescription =await _prescriptionRepository.GetByIdAsync(request.Id);
        if(prescription is null)
            return Error.NotFound("Prescription.NotFound", $"No prescription found with ID {request.Id}");

        return new PrescriptionDto
        (
            prescription.Id,
            prescription.MedicalRecordId,
            $"{prescription.Doctor.FirstName} {prescription.Doctor.LastName}",
            $"{prescription.Patient.FirstName} {prescription.Patient.LastName}",
            prescription.PrescriptionDate,
            prescription.Notes,
            prescription.PrescriptionMedications.Select(pm => new PrescriptionMedicationDto
            (pm.MedicationName,
             pm.Dosage,
             pm.Frequency,
             pm.Duration,
             pm.Instructions)).ToList()
        );
    }
}