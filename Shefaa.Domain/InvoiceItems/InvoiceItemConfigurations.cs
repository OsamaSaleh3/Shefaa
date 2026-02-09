using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.InvoiceItems
{
    public class InvoiceItemConfigurations : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__InvoiceI__3214EC07ECF9E8A2");

            builder.Property(e => e.Description).HasMaxLength(255);
            builder.Property(e => e.Quantity).HasDefaultValue(1);
            builder.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([Quantity]*[UnitPrice])", false)
                .HasColumnType("decimal(29, 2)");
            builder.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Invoice).WithMany(p => p.InvoiceItems)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_InvoiceItems_Invoices");
        }
    }
}
