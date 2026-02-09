using ErrorOr;
using Shefaa.Domain.Appointments;
using Shefaa.Domain.Patients;
using Shefaa.Domain.Prescriptions;
using Shefaa.Domain.Users;

namespace Shefaa.Domain.MedicalRecords;

public partial class MedicalRecord: BaseEntity
{

    public int PatientId { get; private set; }

    public string DoctorId { get; private set; } = null!;

    public int? AppointmentId { get; private set; }

    public DateTime? VisitDate { get; private set; }

    public string ChiefComplaint { get; private set; } = null!;

    public string Symptoms { get; private set; } = null!;

    public string Diagnosis { get; private set; } = null!;

    public string? BloodPressure { get; private set; }

    public decimal? Temperature { get; private set; }

    public int? Pulse { get; private set; }

    public int? RespiratoryRate { get; private set; }

    public decimal? Weight { get; private set; }

    public decimal? Height { get; private set; }

    public string? DoctorNotes { get; private set; }

    public virtual Appointment? Appointment { get; private set; }

    public virtual AspNetUser Doctor { get; private set; } = null!;

    public virtual Patient Patient { get; private set; } = null!;

    public virtual List<Prescription> Prescriptions { get; private set; } = new List<Prescription>();


    public MedicalRecord(int patientId,string doctorId,string chiefComplaint, string symptoms, string diagnosis, int? appointmentId = null)
    {


        PatientId = patientId;
        DoctorId = doctorId;
        ChiefComplaint = chiefComplaint;
        Symptoms = symptoms;
        Diagnosis = diagnosis;
        AppointmentId = appointmentId;
        VisitDate = DateTime.Now;

    }

    public void UpdateVitals(string? bloodPresure=null, decimal? temperature=null, int? pulse=null, int? respiratoryRate=null, decimal? weight=null, decimal? height=null)
    {
        BloodPressure = bloodPresure;
        Temperature = temperature;
        Pulse = pulse;
        RespiratoryRate = respiratoryRate;
        Weight = weight;
        Height = height;
        MarkAsUpdated();
    }

    public ErrorOr<MedicalRecord>Create(int patientId, string doctorId, string chiefComplaint, string symptoms, string diagnosis, int? appointmentId = null)
    {
        if (string.IsNullOrWhiteSpace(chiefComplaint))
        {
            return MedicalRecordErrors.EmptyComplaint;
        }

        if (string.IsNullOrWhiteSpace(diagnosis))
        {
            return MedicalRecordErrors.EmptyDiagnosis;
        }

        return new MedicalRecord(patientId, doctorId, chiefComplaint, symptoms, diagnosis, appointmentId);
    }
    public ErrorOr<Success> UpdateDiagnosis(string newDiagnosis,string? additionalNotes)
    {
        if (string.IsNullOrWhiteSpace(newDiagnosis))
        {
            return MedicalRecordErrors.EmptyDiagnosis;
        }
        Diagnosis = newDiagnosis;

        if(!string.IsNullOrEmpty(additionalNotes))
        {
            DoctorNotes= $"\n[Updated at {DateTime.Now}]: {additionalNotes}";
        }
        MarkAsUpdated();
        return Result.Success;
    }

}
