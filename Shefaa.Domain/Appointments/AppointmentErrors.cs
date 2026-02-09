using ErrorOr;

namespace Shefaa.Domain.Appointments
{
    public static class AppointmentErrors
    {
        public static readonly Error AlreadyCancelled = Error.Validation(
        code: "Appointment.AlreadyCancelled",
        description: "The appointment is already cancelled and cannot be modified.");

        public static readonly Error CannotCompleteCancelled = Error.Validation(
            code: "Appointment.CannotCompleteCancelled",
            description: "A cancelled appointment cannot be marked as completed.");

        public static readonly Error PastDate = Error.Validation(
            code: "Appointment.PastDate",
            description: "The appointment date cannot be set in the past.");

        public static readonly Error DateNotChanged = Error.Validation(
            code: "Appointment.DateNotChanged",
            description: "The new appointment date must be different from the current date.");
    }
}
