using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Prescriptions.Dtos;

namespace Shefaa.Application.Prescriptions.Queries.GetPrescriptionsByMedicalRecord;

public class GetPrescriptionsByMedicalRecordQueryHandler : IRequestHandler<GetPrescriptionsByMedicalRecordQuery, ErrorOr<List<PrescriptionDto>>>
{
    private readonly IMedicalRecordRepository _medicalRecordRepository;
    private readonly IPrescriptionRepository _prescriptionRepository;

    public GetPrescriptionsByMedicalRecordQueryHandler(IPrescriptionRepository prescriptionRepository, IMedicalRecordRepository medicalRecordRepository)
    {
        _prescriptionRepository = prescriptionRepository;
        _medicalRecordRepository = medicalRecordRepository;
    }

    public async  Task<ErrorOr<List<PrescriptionDto>>> Handle(GetPrescriptionsByMedicalRecordQuery request, CancellationToken cancellationToken)
    {
        var medicalRecord = await _medicalRecordRepository.GetByIdAsync(request.MedicalRecordId);
        if(medicalRecord is null)
            return Error.NotFound("MedicalRecord.NotFound", $"Medical record with id {request.MedicalRecordId} not found.");

        var prescriptions = await _prescriptionRepository.GetByMedicalRecordIdAsync(request.MedicalRecordId);

        return prescriptions.Select(p => new PrescriptionDto(
               p.Id,
               p.MedicalRecordId,
               $"{p.Doctor.FirstName} {p.Doctor.LastName}",
               $"{p.Patient.FirstName} {p.Patient.LastName}",
               p.PrescriptionDate,
               p.Notes,
               p.PrescriptionMedications.Select(pm => new PrescriptionMedicationDto(
                   pm.MedicationName,
                   pm.Dosage,
                   pm.Frequency,
                   pm.Duration,
                   pm.Instructions

                   )).ToList()

           )).ToList();
    }
}