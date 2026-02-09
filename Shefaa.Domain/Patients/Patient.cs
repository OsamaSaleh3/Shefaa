using Shefaa.Domain.Appointments;
using Shefaa.Domain.Invoices;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Patients.enums;
using Shefaa.Domain.Prescriptions;
using System;
using System.Collections.Generic;

namespace Shefaa.Domain.Patients;

public partial class Patient: BaseEntity
{

    public string FileNumber { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public Gender Gender { get; set; }

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string Address { get; set; } = null!;

    public BloodType? BloodType { get; set; }

    public string EmergencyContactName { get; set; } = null!;

    public string EmergencyContactPhone { get; set; } = null!;

    public string? GeneralNotes { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();
                   
    public virtual List<Invoice> Invoices { get; set; } = new List<Invoice>();
                   
    public virtual List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();
                   
    public virtual List<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public Patient(string fileNumber,string FrstName,string lastName,DateOnly dob,Gender gender,string phone,string address,string emergencyContactName, string emergencyContactPhone)
    {

        if (string.IsNullOrWhiteSpace(fileNumber))
        {

        }
        if (dob > DateOnly.FromDateTime(DateTime.Now))
        {

        }
            FileNumber = fileNumber;
        FirstName = FrstName;
        LastName = lastName;
        DateOfBirth = dob;
        Gender = gender;
        Phone = phone;
        Address = address;
        EmergencyContactName = emergencyContactName;
        EmergencyContactPhone=emergencyContactPhone;
    }

    public void UpdateContactInfo(string phone,string address, string? email)
    {
        Phone = phone;
        Address = address;
        Email = email;
        MarkAsUpdated();

    }

    public void UpdateEmergencyContact(string name,string phone)
    {
        EmergencyContactName = name;
        EmergencyContactPhone = phone;
        MarkAsUpdated();
    }

    public int GetAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth > today.AddYears(-age)) age--;
        return age;
    }






}
