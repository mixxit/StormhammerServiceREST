using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormhammerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Configuration
{
    public class ZoneConfiguration : IEntityTypeConfiguration<Zone>
    {
        public void Configure(EntityTypeBuilder<Zone> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(80).IsRequired();
            builder.Property(c => c.SafeX).IsRequired().HasDefaultValueSql("0");
            builder.Property(c => c.SafeY).IsRequired().HasDefaultValueSql("0");
            builder.Property(c => c.SafeZ).IsRequired().HasDefaultValueSql("0");
        }
    }
}
