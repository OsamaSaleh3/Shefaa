using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.MedicalRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Queries.GetPatientMedicalHistory
{
    public class GetPatientMedicalHistoryQueryHandler:IRequestHandler<GetPatientMedicalHistoryQuery,ErrorOr<List<MedicalRecordDto>>>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;
        private readonly IPatientRepository _patientRepository;

        public GetPatientMedicalHistoryQueryHandler(IMedicalRecordRepository medicalRecordRepository, IPatientRepository patientRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
            _patientRepository = patientRepository;
        }

        public async Task<ErrorOr<List<MedicalRecordDto>>> Handle(GetPatientMedicalHistoryQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.PatientId);
            if (patient == null)
            {
                return Error.NotFound($"Patient with Id {request.PatientId} not found");
            }

            var medicalRecords = await _medicalRecordRepository.GetPatientMedicalRecords(request.PatientId);

            return medicalRecords.Select(m => new MedicalRecordDto(
                   m.Id,
                   m.PatientId,
                   $"{m.Patient.FirstName} {m.Patient.LastName}",
                   $"{m.Doctor.FirstName} {m.Doctor.LastName}",
                   m.VisitDate,
                   m.ChiefComplaint,
                   m.Diagnosis,
                   m.BloodPressure,
                   m.Temperature))
             .ToList();

        }
    }
}
