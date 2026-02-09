using ErrorOr;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Patients
{
    public static class PatientErrors
    {
        public static readonly Error EmptyFileNumber = Error.Validation(
        code: "Patient.EmptyFileNumber",
        description: "File number is required and cannot be empty.");

        public static readonly Error FutureDateOfBirth = Error.Validation(
            code: "Patient.FutureDateOfBirth",
            description: "Date of birth cannot be in the future.");

        public static readonly Error InvalidContactInfo = Error.Validation(
            code: "Patient.InvalidContactInfo",
            description: "Phone number and address are required for patient contact.");

        public static readonly Error InvalidEmergencyContact = Error.Validation(
            code: "Patient.InvalidEmergencyContact",
            description: "Emergency contact name and phone are required.");
    }
}
