using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.MedicalRecords
{
    public class MedicalRecordConfigurations : IEntityTypeConfiguration<MedicalRecord>
    {
        public void Configure(EntityTypeBuilder<MedicalRecord> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__MedicalR__3214EC0744CC47CF");

            builder.Property(e => e.BloodPressure).HasMaxLength(20);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.DoctorId).HasMaxLength(128);
            builder.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            builder.Property(e => e.Temperature).HasColumnType("decimal(4, 1)");
            builder.Property(e => e.VisitDate).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            builder.HasOne(d => d.Appointment).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK_MedicalRecords_Appointments");

            builder.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecords_Doctors");

            builder.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecords_Patients");
        }
    }
}
