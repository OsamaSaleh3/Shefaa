using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Prescriptions
{
    public static class PrescriptionErrors
    {
        public static readonly Error DuplicateMedication = Error.Conflict(
        code: "Prescription.DuplicateMedication",
        description: "This medication is already included in the prescription.");

        public static readonly Error EmptyMedicationList = Error.Validation(
            code: "Prescription.EmptyMedicationList",
            description: "A prescription must contain at least one medication before it can be finalized.");

        public static readonly Error NotFoundMedication = Error.NotFound(
            code: "Prescription.MedicationNotFound",
            description: "Medication not found in the Prescription.");


    }
}
