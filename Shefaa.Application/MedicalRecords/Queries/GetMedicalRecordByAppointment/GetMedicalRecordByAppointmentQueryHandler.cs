using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.MedicalRecords.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Queries.GetMedicalRecordByAppointment
{
    
    public class GetMedicalRecordByAppointmentQueryHandler : IRequestHandler<GetMedicalRecordByAppointmentQuery, ErrorOr<MedicalRecordDto>>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public GetMedicalRecordByAppointmentQueryHandler(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<ErrorOr<MedicalRecordDto>> Handle(GetMedicalRecordByAppointmentQuery request, CancellationToken cancellationToken)
        {
            var medicalRecord=await _medicalRecordRepository.GetAppointmentMedicalRecord(request.AppointmentId);
            if(medicalRecord is null)
            {
                return Error.NotFound($"medical record with id:{request.AppointmentId} not found");
            }
            return new MedicalRecordDto(
                   medicalRecord.Id,
                   medicalRecord.PatientId,
                   $"{medicalRecord.Patient.FirstName} {medicalRecord.Patient.LastName}",
                   $"{medicalRecord.Doctor.FirstName} {medicalRecord.Doctor.LastName}",
                   medicalRecord.VisitDate,
                   medicalRecord.ChiefComplaint,
                   medicalRecord.Diagnosis,
                   medicalRecord.BloodPressure,
                   medicalRecord.Temperature
                );
        }
    }
}
