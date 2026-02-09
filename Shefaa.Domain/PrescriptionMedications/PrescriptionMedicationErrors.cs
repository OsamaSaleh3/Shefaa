using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.PrescriptionMedications
{
    public static class PrescriptionMedicationErrors
    {
        public static readonly Error EmptyMedicationName = Error.Validation(
        code: "PrescriptionMedication.EmptyMedicationName",
        description: "Medication name is required.");

        public static readonly Error InvalidDosage = Error.Validation(
            code: "PrescriptionMedication.InvalidDosage",
            description: "Dosage information is required and cannot be empty.");

        public static readonly Error InvalidFrequency = Error.Validation(
            code: "PrescriptionMedication.InvalidFrequency",
            description: "Medication frequency (e.g., twice a day) is required.");

        public static readonly Error InvalidDuration = Error.Validation(
            code: "PrescriptionMedication.InvalidDuration",
            description: "Medication duration (e.g., 7 days) is required.");
    }
}
