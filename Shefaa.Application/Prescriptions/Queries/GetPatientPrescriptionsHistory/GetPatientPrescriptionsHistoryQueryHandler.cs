using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Prescriptions.Dtos;

namespace Shefaa.Application.Prescriptions.Queries.GetPatientPrescriptionsHistory;

public class GetPatientPrescriptionsHistoryQueryHandler : IRequestHandler<GetPatientPrescriptionsHistoryQuery, ErrorOr<List<PrescriptionDto>>>
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPatientRepository _patientRepository;

    public GetPatientPrescriptionsHistoryQueryHandler(IPatientRepository patientRepository, IPrescriptionRepository prescriptionRepository)
    {
        _patientRepository = patientRepository;
        _prescriptionRepository = prescriptionRepository;
    }

    public async Task<ErrorOr<List<PrescriptionDto>>> Handle(GetPatientPrescriptionsHistoryQuery request, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(request.PatientId);
        if(patient is null)
            return Error.NotFound("Patient.NotFound", $"No patient found with ID {request.PatientId}");
        
        var patientPrescriptions=await _prescriptionRepository.GetByPatientIdAsync(request.PatientId);

        return patientPrescriptions.Select(p => new PrescriptionDto(
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