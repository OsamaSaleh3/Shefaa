using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Patients
{
    public class PatientConfigurations : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Patients__3214EC07F7A2956C");

            builder.HasIndex(e => e.FileNumber, "UQ__Patients__8BD00B71E6FD016F").IsUnique();

            builder.Property(e=>e.Gender).HasConversion<string>();

            builder.Property(e => e.Address).HasMaxLength(255);
            builder.Property(e => e.BloodType).HasMaxLength(5);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Email).HasMaxLength(100);
            builder.Property(e => e.EmergencyContactName).HasMaxLength(100);
            builder.Property(e => e.EmergencyContactPhone).HasMaxLength(20);
            builder.Property(e => e.FileNumber).HasMaxLength(20);
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.Gender).HasMaxLength(10);
            builder.Property(e => e.IsDeleted).HasDefaultValue(false);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.Phone).HasMaxLength(20);
        }
    }
}
