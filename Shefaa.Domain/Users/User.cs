using Microsoft.AspNetCore.Identity;
using Shefaa.Domain.Appointments;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Prescriptions;

namespace Shefaa.Domain.Users;

public partial class User : IdentityUser
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Specialization { get; set; } 

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;


    public virtual List<Appointment> Appointments { get; set; } = new();

    public virtual List<MedicalRecord> MedicalRecords { get; set; } = new();

    public virtual List<Prescription> Prescriptions { get; set; } = new();
}