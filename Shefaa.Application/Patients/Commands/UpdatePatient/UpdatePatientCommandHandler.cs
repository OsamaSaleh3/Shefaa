using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.Patients.Commands.UpdatePatient
{
    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, ErrorOr<Success>>
    {
        private readonly IPatientRepository _patientRepository;

        public UpdatePatientCommandHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientRepository.GetByIdAsync(request.Id);

            if(patient is null)
            {
                return Error.NotFound(code: "PatientNotFound", description: $"Patient with id : {request.Id} not FOund!");
            }

            patient.UpdatePersonalInfo(
                request.FirstName,
                request.LastName,
                request.DateOfBirth,
                request.Gender
                );

            patient.UpdateContactInfo(
                request.Phone,
                request.Address,
                request.Email
                );

            patient.UpdateEmergencyContact(
                request.EmergencyContactName,
                request.EmergencyContactPhone
                );

            patient.UpdateMedicalDetails(
                request.BloodType,
                request.GeneralNotes
                );

            await _patientRepository.UpdateAsync(patient);

            return Result.Success;
        }
    }
}
