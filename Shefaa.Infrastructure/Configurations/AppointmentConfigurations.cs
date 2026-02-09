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
            
                builder.HasKey(e => e.Id).HasName("PK__Appointm__3214EC07E9F7A69C");
                
                builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
                builder.Property(e => e.DoctorId).HasMaxLength(128);
                builder.Property(e => e.DurationMinutes).HasDefaultValue(30);
            builder.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .HasDefaultValue("Scheduled");

            builder.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointments_Doctors");

            builder.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointments_Patients");
           
        }
    }
}
