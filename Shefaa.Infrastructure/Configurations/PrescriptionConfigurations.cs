using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.Prescriptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Infrastructure.Configurations
{
    public class PrescriptionConfigurations : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.PrescriptionDate).HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.Notes).HasMaxLength(500);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.PatientId).IsRequired();
            builder.Property(e => e.DoctorId).IsRequired();
            builder.Property(e => e.MedicalRecordId).IsRequired();

            builder.HasOne(p => p.Patient)
                .WithMany(pa => pa.Prescriptions)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Prescriptions_Patients");

            builder.HasOne(p => p.Doctor)
                .WithMany(u => u.Prescriptions)
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Prescriptions_Doctors");

            builder.HasOne(p => p.MedicalRecord)
                .WithMany(m => m.Prescriptions)
                .HasForeignKey(p => p.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Prescriptions_MedicalRecords");

            builder.HasMany(p => p.PrescriptionMedications)
                .WithOne(pm => pm.Prescription)
                .HasForeignKey(pm => pm.PrescriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
