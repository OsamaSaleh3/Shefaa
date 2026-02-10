using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Infrastructure.Configurations
{
    public class MedicalRecordConfigurations : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ChiefComplaint).HasMaxLength(500).IsRequired();
            builder.Property(e => e.Symptoms).HasMaxLength(1000).IsRequired();
            builder.Property(e => e.Diagnosis).HasMaxLength(1000).IsRequired();
            builder.Property(e => e.BloodPressure).HasMaxLength(20);
            builder.Property(e => e.Temperature).HasColumnType("decimal(4, 1)");
            builder.Property(e => e.Pulse).HasDefaultValue(0);
            builder.Property(e => e.RespiratoryRate).HasDefaultValue(0);
            builder.Property(e => e.Weight).HasColumnType("decimal(5, 2)");
            builder.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            builder.Property(e => e.DoctorNotes).HasMaxLength(1000);
            builder.Property(e => e.VisitDate).HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.PatientId).IsRequired();
            builder.Property(e => e.DoctorId).IsRequired();

            builder.HasOne(m => m.Patient)
                .WithMany(p => p.MedicalRecords)
                .HasForeignKey(m => m.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MedicalRecords_Patients");

            builder.HasOne(m => m.Doctor)
                .WithMany(u => u.MedicalRecords)
                .HasForeignKey(m => m.DoctorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_MedicalRecords_Doctors");

            builder.HasOne(m => m.Appointment)
                .WithMany(a => a.MedicalRecords)
                .HasForeignKey(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_MedicalRecords_Appointments");

            builder.HasMany(m => m.Prescriptions)
                .WithOne(p => p.MedicalRecord)
                .HasForeignKey(p => p.MedicalRecordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
