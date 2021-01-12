using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormhammerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Configuration
{
    /*public class IdentityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex("ObjectId").IsUnique();
            builder.Property(c => c.SessionId).HasMaxLength(128);
            builder.Property(c => c.ObjectId).HasMaxLength(128).IsRequired();
        }
    }*/
}
