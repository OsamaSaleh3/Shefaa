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
    public string FileNumber { get; private set; } = null!;

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public DateOnly DateOfBirth { get; private set; }

    public Gender Gender { get; private set; }

    public string Phone { get; private set; } = null!;

    public string? Email { get; private set; }

    public string Address { get; private set; } = null!;

    public BloodType? BloodType { get; private set; }

    public string EmergencyContactName { get; private set; } = null!;

    public string EmergencyContactPhone { get; private set; } = null!;

    public string? GeneralNotes { get; private set; }

    public virtual List<Appointment> Appointments { get; private set; } = new List<Appointment>();

    public virtual List<Invoice> Invoices { get; private set; } = new List<Invoice>();

    public virtual List<MedicalRecord> MedicalRecords { get; private set; } = new List<MedicalRecord>();

    public virtual List<Prescription> Prescriptions { get; private set; } = new List<Prescription>();

    internal Patient()
    {
    }

    private Patient(string fileNumber, string firstName, string lastName, DateOnly dateOfBirth, Gender gender, string phone, string address, string emergencyContactName, string emergencyContactPhone)
    {
        FileNumber = fileNumber;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Gender = gender;
        Phone = phone;
        Address = address;
        EmergencyContactName = emergencyContactName;
        EmergencyContactPhone = emergencyContactPhone;
    }

    public static ErrorOr<Patient> Create(string fileNumber, string firstName, string lastName, DateOnly dateOfBirth, Gender gender, string phone, string address, string emergencyContactName, string emergencyContactPhone)
    {
        if (string.IsNullOrWhiteSpace(fileNumber))
            return PatientErrors.EmptyFileNumber;

        if (dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
            return PatientErrors.FutureDateOfBirth;

        if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            return PatientErrors.InvalidContactInfo;

        if (string.IsNullOrWhiteSpace(emergencyContactName) || string.IsNullOrWhiteSpace(emergencyContactPhone))
            return PatientErrors.InvalidEmergencyContact;

        return new Patient(fileNumber, firstName, lastName, dateOfBirth, gender, phone, address, emergencyContactName, emergencyContactPhone);
    }

    public ErrorOr<Success> UpdateContactInfo(string phone, string address, string? email)
    {
        if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            return PatientErrors.InvalidContactInfo;
        
        Phone = phone;
        Address = address;
        Email = email;
        MarkAsUpdated();
        return Result.Success;
    }

    public ErrorOr<Success> UpdateEmergencyContact(string name, string phone)
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

    public override void SoftDelete()
    {
        IsDeleted = true;
        DeletedAt = DateTime.Now;
        MarkAsUpdated();
    }
}
