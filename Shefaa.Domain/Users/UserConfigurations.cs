using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Users
{
    public class UserConfigurations : IEntityTypeConfiguration<AspNetUser>
    {
        public void Configure(EntityTypeBuilder<AspNetUser> builder)
        {

            builder.HasKey(e => e.Id).HasName("PK__AspNetUs__3214EC0724209140");

            builder.Property(e => e.Id).HasMaxLength(128);
            builder.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            builder.Property(e => e.Email).HasMaxLength(256);
            builder.Property(e => e.FirstName).HasMaxLength(50).HasDefaultValue("");
            builder.Property(e => e.LastName).HasMaxLength(50).HasDefaultValue("");
            builder.Property(e => e.IsActive).HasDefaultValue(true);
            builder.Property(e => e.NormalizedEmail).HasMaxLength(256);
            builder.Property(e => e.NormalizedUserName).HasMaxLength(256);
            builder.Property(e => e.Specialization).HasMaxLength(100);
            builder.Property(e => e.UserName).HasMaxLength(256);

            builder.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.IndexerProperty<string>("UserId").HasMaxLength(128);
                        j.IndexerProperty<string>("RoleId").HasMaxLength(128);
                    });
        }
    }
}
