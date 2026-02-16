using ErrorOr;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Patients;
using Shefaa.Domain.PrescriptionMedications;
using Shefaa.Domain.Users;

namespace Shefaa.Domain.Prescriptions;

public partial class Prescription: BaseEntity
{

    public Guid MedicalRecordId { get; private set; }

    public Guid PatientId { get; private set; }

    public string DoctorId { get; private set; } = null!;

    public DateTime? PrescriptionDate { get; private set; }

    public string? Notes { get; private set; }

    public User Doctor { get; private set; } = null!;

    public MedicalRecord MedicalRecord { get; private set; } = null!;

    public Patient Patient { get; private set; } = null!;

    public List<PrescriptionMedication> PrescriptionMedications { get; private set; } = new List<PrescriptionMedication>();

    internal Prescription()
    {
    }

    private Prescription(Guid medicalRecordId, Guid patientId, string doctorId, string? notes = null)
    {
        MedicalRecordId = medicalRecordId;
        PatientId = patientId;
        DoctorId = doctorId;
        Notes = notes;
    }

    public static ErrorOr<Prescription> Create(Guid medicalRecordId, Guid patientId, string doctorId, string? notes = null)
    {
        return new Prescription(medicalRecordId, patientId, doctorId, notes);
    }

    public ErrorOr<Success> AddMedication(string medicationName, string dosage, string frequency, string duration, string? instructions = null)
    {
        if (PrescriptionMedications.Any(m => m.MedicationName.Equals(medicationName, StringComparison.OrdinalIgnoreCase)))
            return PrescriptionErrors.DuplicateMedication;

        var medicationResult = PrescriptionMedication.Create(medicationName, dosage, frequency, duration, instructions);
        if (medicationResult.IsError)
            return medicationResult.Errors;

        PrescriptionMedications.Add(medicationResult.Value);
        MarkAsUpdated();
        return Result.Success;
    }
}
