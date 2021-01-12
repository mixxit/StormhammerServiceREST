using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormhammerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Configuration
{
    public class IdentityConfiguration : IEntityTypeConfiguration<Identity>
    {
        public void Configure(EntityTypeBuilder<Identity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex("UniqueId").IsUnique();
            builder.HasIndex("Username").IsUnique();
            builder.Property(c => c.UniqueId).HasMaxLength(128).IsRequired();
            builder.Property(c => c.Username).HasMaxLength(36);
            builder.Property(c => c.Password).HasMaxLength(128);
            builder.Property(c => c.SessionId).HasMaxLength(128);
        }
    }
}
