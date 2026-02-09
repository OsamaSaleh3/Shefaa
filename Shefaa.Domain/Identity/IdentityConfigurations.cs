using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shefaa.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shefaa.Domain.Identity
{
    public class IdentityConfigurations :
        IEntityTypeConfiguration<AspNetRole>,
        IEntityTypeConfiguration<AspNetRoleClaim>,
        IEntityTypeConfiguration<AspNetUserClaim>,
        IEntityTypeConfiguration<AspNetUserLogin>,
        IEntityTypeConfiguration<AspNetUserToken>
    {
        public void Configure(EntityTypeBuilder<AspNetRole> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__AspNetRo__3214EC079DD135E2");
            builder.Property(e => e.Id).HasMaxLength(128);
            builder.Property(e => e.Name).HasMaxLength(256);
            builder.Property(e => e.NormalizedName).HasMaxLength(256);
        }

        public void Configure(EntityTypeBuilder<AspNetRoleClaim> builder)
        {
            builder.Property(e => e.RoleId).HasMaxLength(128);
            builder.HasOne(d => d.Role)
                   .WithMany(p => p.AspNetRoleClaims)
                   .HasForeignKey(d => d.RoleId);
        }

        

        public void Configure(EntityTypeBuilder<AspNetUserClaim> builder)
        {
            builder.Property(e => e.UserId).HasMaxLength(128);
            builder.HasOne(d => d.User)
                   .WithMany(p => p.AspNetUserClaims)
                   .HasForeignKey(d => d.UserId);
        }

        public void Configure(EntityTypeBuilder<AspNetUserLogin> builder)
        {
            builder.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            builder.Property(e => e.LoginProvider).HasMaxLength(128);
            builder.Property(e => e.ProviderKey).HasMaxLength(128);
            builder.Property(e => e.UserId).HasMaxLength(128);
            builder.HasOne(d => d.User)
                   .WithMany(p => p.AspNetUserLogins)
                   .HasForeignKey(d => d.UserId);
        }

        public void Configure(EntityTypeBuilder<AspNetUserToken> builder)
        {
            builder.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            builder.Property(e => e.UserId).HasMaxLength(128);
            builder.Property(e => e.LoginProvider).HasMaxLength(128);
            builder.Property(e => e.Name).HasMaxLength(128);
            builder.HasOne(d => d.User)
                   .WithMany(p => p.AspNetUserTokens)
                   .HasForeignKey(d => d.UserId);
        }
    }
}
