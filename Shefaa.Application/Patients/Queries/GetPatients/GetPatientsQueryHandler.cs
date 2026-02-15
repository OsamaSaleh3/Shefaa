using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Application.Patients.Dtos;
using Shefaa.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Patients.Queries.GetPatients
{
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, ErrorOr<List<PatientDto>>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<ErrorOr<List<PatientDto>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _patientRepository.GetAllAsync();
            if (patients.Count < 1)
            {
                return Error.NotFound(code: "Patient.NotFound", description: "there is no patient found");
            }
            
            return patients.Select(patient=>new PatientDto(
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
                )).ToList();
        }
    }
}
