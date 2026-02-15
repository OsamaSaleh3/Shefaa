using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using Shefaa.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Patients.Commands.CreatePatient
{
    public class CreatepatientCommandHandler : IRequestHandler<CreatePatientCommand, ErrorOr<Guid>>
    {
        private readonly IPatientRepository _patienRepository;

        public CreatepatientCommandHandler(IPatientRepository patienRepository)
        {
            _patienRepository = patienRepository;
        }

        public async Task<ErrorOr<Guid>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientResult = Patient.Create(
                GenerateFileNumber(),
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.Gender,
                request.PhoneNumber,
                request.Address,
                request.EmergencyContactName,
                request.EmergencyContactPhone
                );

            if (patientResult.IsError)
            {
                return patientResult.Errors;
            }

            var patient = patientResult.Value;

            if (!string.IsNullOrEmpty(request.Email))
            {
                patient.UpdateContactInfo(request.PhoneNumber, request.Address, request.Email);
            }

            if(request.BloodType.HasValue || !string.IsNullOrEmpty(request.GeneralNotes))
            {
                patient.UpdateMedicalDetails(request.BloodType, request.GeneralNotes);
            }

            await _patienRepository.CreateAsync(patient);
            return patient.Id;
        }
        
    
        private string GenerateFileNumber()
        {
            return $"PAT-{DateTime.Now:yyyyMMdd}-{Guid.NewGuid().ToString().Substring(0, 4).ToUpper()}";
        }
    }
}
