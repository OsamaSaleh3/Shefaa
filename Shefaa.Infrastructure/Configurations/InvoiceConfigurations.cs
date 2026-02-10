using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.Invoices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Infrastructure.Configurations
{
    public class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.InvoiceNumber).IsUnique();

            builder.Property(e => e.InvoiceNumber).HasMaxLength(20).IsRequired();
            builder.Property(e => e.InvoiceDate).HasDefaultValueSql("GETDATE()");
            builder.Property(e => e.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasDefaultValueSql("'Unpaid'");
            builder.Property(e => e.PaymentMethod).HasConversion<string>().HasMaxLength(50);
            builder.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasDefaultValue(0m);
            builder.Property(e => e.PaidAmount)
                .HasColumnType("decimal(18, 2)")
                .HasDefaultValue(0m);
            builder.Property(e => e.RemainingAmount)
                .HasColumnType("decimal(18, 2)")
                .HasComputedColumnSql("[TotalAmount] - [PaidAmount]", stored: true);
            builder.Property(e => e.Notes).HasMaxLength(500);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.PatientId).IsRequired();

            builder.HasOne(i => i.Patient)
                .WithMany(p => p.Invoices)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_Invoices_Patients");

            builder.HasMany(i => i.InvoiceItems)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
