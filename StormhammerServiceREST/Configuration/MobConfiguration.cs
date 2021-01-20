using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StormhammerLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST.Configuration
{
    public class MobConfiguration : IEntityTypeConfiguration<Mob>
    {
        public void Configure(EntityTypeBuilder<Mob> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.MobRaceId).IsRequired();
            builder.Property(c => c.MobClassId).IsRequired();
            builder.Property(c => c.AccountId);
            builder.Property(c => c.ZoneId).HasDefaultValueSql("1");
            builder.Property(c => c.X).HasDefaultValueSql("0");
            builder.Property(c => c.Y).HasDefaultValueSql("0");
            builder.Property(c => c.Z).HasDefaultValueSql("0");

            builder.HasOne<Zone>().WithMany().HasForeignKey(c => c.ZoneId);
            builder.HasOne<Account>().WithMany().HasForeignKey(c => c.AccountId);
            builder.HasOne<MobClass>().WithMany().HasForeignKey(c => c.MobClassId);
            builder.HasOne<MobRace>().WithMany().HasForeignKey(c => c.MobRaceId);
        }
    }
}
