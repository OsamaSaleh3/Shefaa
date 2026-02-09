using ErrorOr;
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

    private Patient(string fileNumber,string firstName,string lastName,DateOnly dob,Gender gender,string phone,string address,string emergencyContactName, string emergencyContactPhone)
    {

       
            FileNumber = fileNumber;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dob;
        Gender = gender;
        Phone = phone;
        Address = address;
        EmergencyContactName = emergencyContactName;
        EmergencyContactPhone=emergencyContactPhone;
    }

    public ErrorOr<Patient> Create(string fileNumber, string firstName, string lastName, DateOnly dob, Gender gender, string phone, string address, string emergencyContactName, string emergencyContactPhone)
    {
        if (string.IsNullOrWhiteSpace(fileNumber))
            return PatientErrors.EmptyFileNumber;

        if (dob > DateOnly.FromDateTime(DateTime.Now))
            return PatientErrors.FutureDateOfBirth;

        if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            return PatientErrors.InvalidContactInfo;

        if (string.IsNullOrWhiteSpace(emergencyContactName) || string.IsNullOrWhiteSpace(emergencyContactPhone))
            return PatientErrors.InvalidEmergencyContact;

        return new Patient(fileNumber, firstName, lastName, dob, gender, phone, address, emergencyContactName, emergencyContactPhone);
    }

    public ErrorOr<Success>UpdateContactInfo(string phone,string address, string? email)
    {
        if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            return PatientErrors.InvalidContactInfo;
        Phone = phone;
        Address = address;
        Email = email;
        MarkAsUpdated();
        return Result.Success;

    }

    public ErrorOr<Success> UpdateEmergencyContact(string name,string phone)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone))
            return PatientErrors.InvalidEmergencyContact;
        EmergencyContactName = name;
        EmergencyContactPhone = phone;
        MarkAsUpdated();
        return Result.Success;
    }

    public int GetAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today.Year - DateOfBirth.Year;
        if (DateOfBirth > today.AddYears(-age)) age--;
        return age;
    }






}
