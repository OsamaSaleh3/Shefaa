using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.MedicalRecords
{
    public static class MedicalRecordErrors
    {
        public static readonly Error EmptyComplaint = Error.Validation(
        code: "MedicalRecord.EmptyComplaint",
        description: "Chief complaint cannot be empty. Every medical record must state why the patient is here.");

        public static readonly Error EmptyDiagnosis = Error.Validation(
            code: "MedicalRecord.EmptyDiagnosis",
            description: "Diagnosis cannot be empty. A medical record must include a clinical assessment.");

        public static readonly Error InvalidVitalSigns = Error.Validation(
            code: "MedicalRecord.InvalidVitalSigns",
            description: "One or more vital signs are outside the logically possible range.");
    }
}
