using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shefaa.Domain.Appointments;
using Shefaa.Domain.InvoiceItems;
using Shefaa.Domain.Invoices;
using Shefaa.Domain.MedicalRecords;
using Shefaa.Domain.Patients;
using Shefaa.Domain.PrescriptionMedications;
using Shefaa.Domain.Prescriptions;
using Shefaa.Domain.Users;

namespace Shefaa.Infrastructure.Common.Persistence;

public partial class ShefaaDbContext : IdentityDbContext<User>
{
    public ShefaaDbContext()
    {
    }

    public ShefaaDbContext(DbContextOptions<ShefaaDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=OSAMA-ALMAHSERE;Database=Shefaa;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<PrescriptionMedication> PrescriptionMedications { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShefaaDbContext).Assembly);
    }
}
