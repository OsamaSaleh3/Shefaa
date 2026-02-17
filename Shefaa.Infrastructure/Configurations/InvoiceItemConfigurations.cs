using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.InvoiceItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Infrastructure.Configurations
{
    public class InvoiceItemConfigurations : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description).HasMaxLength(500).IsRequired();
            builder.Property(e => e.Quantity).HasDefaultValue(1);
            builder.Property(e => e.UnitPrice)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();
            builder.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 2)")
                .HasComputedColumnSql("[Quantity] * [UnitPrice]", stored: true);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.InvoiceId).IsRequired();

            builder.HasOne(ii => ii.Invoice)
                .WithMany(i => i.InvoiceItems)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_InvoiceItems_Invoices");
            builder.Property(m => m.Id)
          .ValueGeneratedNever();
        }
    }
}
