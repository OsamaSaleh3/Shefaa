using ErrorOr;
using MediatR;
using Shefaa.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Commands.UpdateDiagnosis
{
    public class UpdateDiagnosisCommandHandler : IRequestHandler<UpdateDiagnosisCommand, ErrorOr<Success>>
    {
        private readonly IMedicalRecordRepository _medicalRecordrepository;

        public UpdateDiagnosisCommandHandler(IMedicalRecordRepository medicalRecordrepository)
        {
            _medicalRecordrepository = medicalRecordrepository;
        }

        public async Task<ErrorOr<Success>> Handle(UpdateDiagnosisCommand request, CancellationToken cancellationToken)
        {
            var medicalRecord = await _medicalRecordrepository.GetByIdAsync(request.Id);
            if(medicalRecord is null)
            {
                return Error.NotFound($"medical Record with id:  {request.Id} not found");
            }
            var UpdateResult=medicalRecord.UpdateDiagnosis(
                request.NewDiagnosis,
                request.AdditionalNotes
                );
            if (UpdateResult.IsError)
            {
                return UpdateResult.Errors;
            }
            await _medicalRecordrepository.UpdateAsync(medicalRecord);
            return Result.Success;
        }
    }
}
