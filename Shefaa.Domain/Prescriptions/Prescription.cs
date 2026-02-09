using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Patients;
using Shefaa.Domain.PrescriptionMedications;
using Shefaa.Domain.Users;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Shefaa.Domain.Prescriptions;

public partial class Prescription: BaseEntity
{

    public int MedicalRecordId { get; set; }

    public int PatientId { get; set; }

    public string DoctorId { get; set; } = null!;

    public DateTime? PrescriptionDate { get; set; }

    public string? Notes { get; set; }

    public virtual AspNetUser Doctor { get; set; } = null!;

    public virtual MedicalRecord MedicalRecord { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;

    public virtual List<PrescriptionMedication> PrescriptionMedications { get; set; } = new List<PrescriptionMedication>();

    public Prescription(int medicalRecordId, int patientId, string doctorId, string? notes=null)
    {
        MedicalRecordId = medicalRecordId;
        PatientId = patientId;
        DoctorId = doctorId;
        Notes = notes;
    }

    public void AddMedication(string medicationName,string dosage,string frequency,string duration,string? instructions=null )
    {
        if (PrescriptionMedications.Any(m => m.MedicationName.Equals(medicationName, StringComparison.OrdinalIgnoreCase)))
            throw new Exception("this medication is already in the list");

        var medication = new PrescriptionMedication(medicationName, dosage, frequency, duration, instructions);
        PrescriptionMedications.Add(medication);
        MarkAsUpdated();
    }
}
