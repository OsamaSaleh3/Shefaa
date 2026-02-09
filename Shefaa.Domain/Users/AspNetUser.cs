using Shefaa.Domain.Appointments;
using Shefaa.Domain.Identity;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Prescriptions;

namespace Shefaa.Domain.Users;

public partial class AspNetUser
{
    public string Id { get; set; } = null!;

    public string? UserName { get; set; }

    public string? NormalizedUserName { get; set; }

    public string? Email { get; set; }

    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Specialization { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual List<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual List<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    public virtual List<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    public virtual List<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    public virtual List<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual List<Prescription> Prescriptions { get; set; } = new List<Prescription>();

    public virtual List<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
