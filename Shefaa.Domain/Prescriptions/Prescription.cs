using ErrorOr;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Patients;
using Shefaa.Domain.PrescriptionMedications;
using Shefaa.Domain.Users;

namespace Shefaa.Domain.Prescriptions;

public partial class Prescription : BaseEntity
{
    public Guid MedicalRecordId { get; private set; }
    public Guid PatientId { get; private set; }
    public string DoctorId { get; private set; } = null!;
    public DateTime PrescriptionDate { get; private set; }
    public string? Notes { get; private set; }

    public User Doctor { get; private set; } = null!;
    public MedicalRecord MedicalRecord { get; private set; } = null!;
    public Patient Patient { get; private set; } = null!;

    private readonly List<PrescriptionMedication> _prescriptionMedications = new();
    public IReadOnlyCollection<PrescriptionMedication> PrescriptionMedications => _prescriptionMedications.AsReadOnly();

    internal Prescription() { }

    private Prescription(Guid medicalRecordId, Guid patientId, string doctorId, string? notes)
    {
        MedicalRecordId = medicalRecordId;
        PatientId = patientId;
        DoctorId = doctorId;
        Notes = notes;
        PrescriptionDate = DateTime.UtcNow;
    }

    public static ErrorOr<Prescription> Create(Guid medicalRecordId, Guid patientId, string doctorId, string? notes = null)
    {
        if (string.IsNullOrWhiteSpace(doctorId))
            return Error.Validation("DoctorId.Empty", "Doctor ID is required.");

        return new Prescription(medicalRecordId, patientId, doctorId, notes);
    }

    public ErrorOr<Success> AddMedication(string medicationName, string dosage, string frequency, string duration, string? instructions = null)
    {
        if (_prescriptionMedications.Any(m => m.MedicationName.Equals(medicationName, StringComparison.OrdinalIgnoreCase)))
            return PrescriptionErrors.DuplicateMedication;

        var medicationResult = PrescriptionMedication.Create(medicationName, dosage, frequency, duration, instructions);
        
        if (medicationResult.IsError)
            return medicationResult.Errors;

        _prescriptionMedications.Add(medicationResult.Value);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> RemoveMedication(string medicationName)
    {
        var medication = _prescriptionMedications.FirstOrDefault(m => m.MedicationName.Equals(medicationName, StringComparison.OrdinalIgnoreCase));
        
        if (medication is null)
            return PrescriptionErrors.NotFoundMedication;

        _prescriptionMedications.Remove(medication);
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> UpdateMedicationDetails(string medicationName, string newDosage, string newFrequency, string newDuration, string? newInstructions)
    {
        var medication = _prescriptionMedications.FirstOrDefault(m => m.MedicationName.Equals(medicationName, StringComparison.OrdinalIgnoreCase));
        
        if (medication is null)
            return PrescriptionErrors.NotFoundMedication;

        var result = medication.UpdateDetails(newDosage, newFrequency, newDuration, newInstructions);
        
        if (result.IsError)
            return result.Errors;

        MarkAsUpdated();
        return Result.Success;
    }

    public void UpdateNotes(string? newNotes)
    {
        Notes = newNotes;
        MarkAsUpdated();
    }
}