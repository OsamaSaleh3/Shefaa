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
            builder.HasKey(e => e.Id).HasName("PK__Prescrip__3214EC07131729EF");

            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.DoctorId).HasMaxLength(128);
            builder.Property(e => e.PrescriptionDate).HasDefaultValueSql("(getdate())");

            builder.HasOne(d => d.Doctor).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_Doctors");

            builder.HasOne(d => d.MedicalRecord).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_Records");

            builder.HasOne(d => d.Patient).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Prescriptions_Patients");
        }
    }
}
