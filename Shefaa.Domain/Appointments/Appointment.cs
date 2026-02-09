using Shefaa.Domain.Appointments.enums;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Patients;
using Shefaa.Domain.Users;
using System;
using System.Collections.Generic;

namespace Shefaa.Domain.Appointments;

public partial class Appointment:BaseEntity
{

    public int PatientId { get; private set; }

    public string DoctorId { get; private set; } = null!;

    public DateTime AppointmentDate { get; private set; }

    public int? DurationMinutes { get; private set; }

    public AppointmentStatus? Status { get; private set; }

    public string? Notes { get; private set; }

    public virtual AspNetUser Doctor { get; private set; } = null!;

    public virtual List<MedicalRecord> MedicalRecords { get; private set; } = new List<MedicalRecord>();

    public virtual Patient Patient { get; private set; } = null!;

    public Appointment(int patientId, string doctorId, DateTime appointmentDate, int? durationMinutes = null)
    {
        PatientId = patientId;
        DoctorId = doctorId;
        AppointmentDate = appointmentDate;
        DurationMinutes = durationMinutes;
        Status = AppointmentStatus.Scheduled;
    }

    public void Complete()
    {
        if(Status== AppointmentStatus.Cancelled)
        {
            
        }
        Status = AppointmentStatus.Completed;
        MarkAsUpdated();
    }

    public void Cancel(string reason)
    {
        if(Status== AppointmentStatus.Cancelled)
        {

        }
        Notes = $"Cancelation reason : {reason}";
        Status = AppointmentStatus.Cancelled;
        MarkAsUpdated();
    }

    public void Reschedule(DateTime newDate)
    {
        if (Status== AppointmentStatus.Cancelled)
        {

        }
        if (newDate < DateTime.Now)
        {

        }
        AppointmentDate = newDate;
        Status = AppointmentStatus.Rescheduled;
        MarkAsUpdated();
    }


}
