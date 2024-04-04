﻿using LoginSystem.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Infra.Contexts.AccountContext.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128)
                .IsRequired(true);

            builder.Property(x => x.Image)
                .HasColumnName("Image")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .Property(x => x.Address)
                .HasColumnName("Email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.Verification)
                .Property(x => x.Code)
                .HasColumnName("EmailVerificationCode")
                .IsRequired(true);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.Verification)
                .Property(x => x.ExpiresAt)
                .HasColumnName("EmailVerificationExpiresAt")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.Verification)
                .Property(x => x.VerifiedAt)
                .HasColumnName("EmailVerificationVerifiedAt")
                .IsRequired(false);

            builder.OwnsOne(x => x.Email)
                .OwnsOne(x => x.Verification)
                .Ignore(x => x.IsActive);

            builder.OwnsOne(x => x.Password)
                .Property(x => x.Hash)
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.OwnsOne(x => x.Password)
                .Property(x => x.ResetCode)
                .HasColumnName("PasswordResetCode")
                .IsRequired();
        }
    }
}
