using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.MedicalRecords.Dtos;
using Shefaa.Application.MedicalRecords.Queries.GetMedicalRecordByAppointment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Queries.GetMedicalRecordById
{
    public class GetMedicalRecordByIdQueryHandler : IRequestHandler<GetMedicalRecordByIdQuery, ErrorOr<MedicalRecordDto>>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public GetMedicalRecordByIdQueryHandler(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<ErrorOr<MedicalRecordDto>> Handle(GetMedicalRecordByIdQuery request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(request.Id);
            if (medicalRecord is null)
            {
                return Error.NotFound($"medical record with Id {request.Id} not found");
            }


            return new MedicalRecordDto(
                   medicalRecord.Id,
                   medicalRecord.PatientId,
                   $"{medicalRecord.Patient.FirstName} {medicalRecord.Patient.LastName}",
                   $"{medicalRecord.Doctor.FirstName}  {medicalRecord.Doctor.LastName}",
                   medicalRecord.VisitDate,
                   medicalRecord.ChiefComplaint,
                   medicalRecord.Diagnosis,
                   medicalRecord.BloodPressure,
                   medicalRecord.Temperature);
        }
    }
}
