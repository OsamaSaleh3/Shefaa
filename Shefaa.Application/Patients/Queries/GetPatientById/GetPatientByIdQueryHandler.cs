using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Patients.Dtos;
using Shefaa.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Patients.Queries.GetPatientById
{
    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, ErrorOr<PatientDto?>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<ErrorOr<PatientDto?>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);
            if (patient is null)
            {
                return Error.NotFound(code: "Patient.NotFound", description: $"Patient with id : {request.Id} not found!");
            }

            return new PatientDto(
                patient.Id,
                patient.FileNumber,
                patient.FirstName,
                patient.LastName,
                patient.FirstName + " " + patient.LastName,
                patient.DateOfBirth,
                patient.GetAge(),
                patient.Gender.ToString(),
                patient.Phone,
                patient.Email ?? "No Email Address Available",
                patient.Address,
                patient.BloodType.ToString() ?? "No Blood Type Available",
                patient.EmergencyContactName,
                patient.EmergencyContactPhone,
                patient.GeneralNotes ?? "No General Note Available"
                );
        }
    }
}
