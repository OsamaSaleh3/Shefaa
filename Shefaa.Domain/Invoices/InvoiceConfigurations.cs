using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Invoices
{
    public class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Invoices__3214EC0757017808");

            builder.HasIndex(e => e.InvoiceNumber, "UQ__Invoices__D776E9812744C552").IsUnique();

            builder.Property(e => e.Status).HasConversion<string>();
            builder.Property(e => e.PaymentMethod).HasConversion<string>();


            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.InvoiceDate).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.InvoiceNumber).HasMaxLength(20);
            builder.Property(e => e.PaidAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            builder.Property(e => e.PaymentMethod).HasMaxLength(50);
            builder.Property(e => e.RemainingAmount)
                .HasComputedColumnSql("([TotalAmount]-[PaidAmount])", false)
                .HasColumnType("decimal(19, 2)");
            builder.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Unpaid");
            builder.Property(e => e.TotalAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Patient).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoices_Patients");
        }
    }
}
