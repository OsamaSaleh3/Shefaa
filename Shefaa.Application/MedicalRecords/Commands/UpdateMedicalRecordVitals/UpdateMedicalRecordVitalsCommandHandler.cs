using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Commands.UpdateMedicalRecordVitals
{
    public class UpdateMedicalRecordVitalsCommandHandler : IRequestHandler<UpdateMedicalRecordVitalsCommand, ErrorOr<Success>>
    {
        private readonly IMedicalRecordRepository _medicalRecordRepository;

        public UpdateMedicalRecordVitalsCommandHandler(IMedicalRecordRepository medicalRecordRepository)
        {
            _medicalRecordRepository = medicalRecordRepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateMedicalRecordVitalsCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordRepository.GetByIdAsync(request.Id);
            if (medicalRecord is null)
            {
                return Error.NotFound($"medical Record with id:  {request.Id} not found");
            }

            medicalRecord.UpdateVitals(request.BloodPressure,
                                                       request.Temperature,
                                                       request.Pulse,
                                                       request.RespiratoryRate,
                                                       request.Weight,
                                                       request.Height);
            await _medicalRecordRepository.UpdateAsync(medicalRecord);
            return Result.Success;
        }
    }
}
