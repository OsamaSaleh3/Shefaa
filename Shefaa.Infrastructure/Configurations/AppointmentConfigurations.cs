using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.Appointments;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Shefaa.Infrastructure.Configurations
{
    public class AppointmentConfigurations : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.AppointmentDate).IsRequired();
            builder.Property(e => e.DurationMinutes).HasDefaultValue(30);
            builder.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValueSql("'Scheduled'");
            builder.Property(e => e.Notes).HasMaxLength(500);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.PatientId).IsRequired();
            builder.Property(e => e.DoctorId).IsRequired();

            builder.HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Appointments_Patients");

            builder.HasOne(a => a.Doctor)
                .WithMany(u => u.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Appointments_Doctors");

            builder.HasMany(a => a.MedicalRecords)
                .WithOne(m => m.Appointment)
                .HasForeignKey(m => m.AppointmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
