using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.PrescriptionMedications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Infrastructure.Configurations
{
    public class PrescriptionMedicationConfigurations : IEntityTypeConfiguration<PrescriptionMedication>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedication> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Prescrip__3214EC07E4ADB5E9");

            builder.Property(e => e.Dosage).HasMaxLength(50);
            builder.Property(e => e.Duration).HasMaxLength(50);
            builder.Property(e => e.Frequency).HasMaxLength(50);
            builder.Property(e => e.MedicationName).HasMaxLength(100);

            builder.HasOne(d => d.Prescription).WithMany(p => p.PrescriptionMedications)
                .HasForeignKey(d => d.PrescriptionId)
                .HasConstraintName("FK_Medications_Prescriptions");

            builder.Property(m => m.Id)
           .ValueGeneratedNever();
        }
    }
}
