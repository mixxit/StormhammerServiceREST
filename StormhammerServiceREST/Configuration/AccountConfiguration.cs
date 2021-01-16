using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormhammerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex("ObjectId").IsUnique();
            builder.Property(c => c.Email).HasMaxLength(320);
            builder.Property(c => c.ObjectId).IsRequired();
        }
    }
}
