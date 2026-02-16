using ErrorOr;
using Shefaa.Domain.Appointments.enums;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Patients;
using Shefaa.Domain.Users;

namespace Shefaa.Domain.Appointments;

public partial class Appointment:BaseEntity
{

    public Guid PatientId { get; private set; }

    public string DoctorId { get; private set; } = null!;

    public DateTime AppointmentDate { get; private set; }

    public int DurationMinutes { get; private set; }

    public AppointmentStatus Status { get; private set; }

    public string? Notes { get; private set; }

    public User Doctor { get; private set; } = null!;

    public List<MedicalRecord> MedicalRecords { get; private set; } = new List<MedicalRecord>();

    public Patient Patient { get; private set; } = null!;

   

    internal Appointment()
    {
    }

    private Appointment(Guid patientId, string doctorId, DateTime appointmentDate, int durationMinutes,string? notes=null)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        AppointmentDate = appointmentDate;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
        Notes = notes;
    }

    public static ErrorOr<Appointment> Create(Guid patientId, string doctorId, DateTime appointmentDate, int durationMinutes,string? notes=null)
    {
        if (appointmentDate < DateTime.Now)
            return AppointmentErrors.InvalidRescheduleDate;

        return new Appointment(patientId, doctorId, appointmentDate, durationMinutes,notes);
    }

    public ErrorOr<Success> Complete()
    {
        if(Status== AppointmentStatus.Cancelled)
        {
            return AppointmentErrors.AlreadyCancelled;
        }
        Status = AppointmentStatus.Completed;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Cancel(string reason)
    {
        if(Status== AppointmentStatus.Cancelled)
        {
            return AppointmentErrors.AlreadyCancelled;
        }
        Notes = $"Cancelation reason : {reason}";
        Status = AppointmentStatus.Cancelled;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> Reschedule(DateTime newDate)
    {
        if (Status== AppointmentStatus.Cancelled)
        {
            return AppointmentErrors.AlreadyCancelled;
        }
        if (newDate < DateTime.Now)
        {
            return AppointmentErrors.InvalidRescheduleDate;
        }
        AppointmentDate = newDate;
        Status = AppointmentStatus.Rescheduled;
        MarkAsUpdated();
        return Result.Success;
    }
}
