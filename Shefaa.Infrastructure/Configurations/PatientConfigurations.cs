using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Infrastructure.Configurations
{
    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.FileNumber).IsUnique();

            builder.Property(e => e.FileNumber).HasMaxLength(20).IsRequired();
            builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();
            builder.Property(e => e.Phone).HasMaxLength(20).IsRequired();
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.Address).HasMaxLength(255).IsRequired();
            builder.Property(e => e.BloodType).HasMaxLength(5);
            builder.Property(e => e.EmergencyContactName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.EmergencyContactPhone).HasMaxLength(20).IsRequired();
            builder.Property(e => e.GeneralNotes).HasMaxLength(500);

            builder.Property(e => e.Gender).HasConversion<string>().HasMaxLength(10).IsRequired();
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.DeletedAt);
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);

            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Invoices)
                .WithOne(i => i.Patient)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.MedicalRecords)
                .WithOne(m => m.Patient)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Prescriptions)
                .WithOne(pr => pr.Patient)
                .HasForeignKey(pr => pr.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
