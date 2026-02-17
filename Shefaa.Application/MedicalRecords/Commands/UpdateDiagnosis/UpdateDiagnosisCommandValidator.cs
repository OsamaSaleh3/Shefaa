using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Application.MedicalRecords.Commands.UpdateDiagnosis
{
    public class UpdateDiagnosisCommandValidator:AbstractValidator<UpdateDiagnosisCommand>
    { 
        public UpdateDiagnosisCommandValidator()
        {
            RuleFor(m => m.Id).NotNull()
                .WithMessage("Medicl Record Id Can Not Be Null");
            RuleFor(m => m.NewDiagnosis).NotNull()
                .WithMessage("Diagnosis Can not be null");
           
        }
    }
}
