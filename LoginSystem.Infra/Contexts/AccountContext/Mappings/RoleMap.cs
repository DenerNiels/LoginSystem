using LoginSystem.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Infra.Contexts.AccountContext.Mappings
{
    internal class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(x=> x.Id);
            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(128)
                .IsRequired(true);
        }
    }
}
