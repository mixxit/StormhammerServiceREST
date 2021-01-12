﻿using Microsoft.EntityFrameworkCore;
using StormhammerLibrary.Models;
using StormhammerServiceREST.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StormhammerServiceREST
{
    public class StormhammerContext : DbContext
    {
        public DbSet<MobClass> MobClass { get; set; }
        public DbSet<MobRace> MobRace { get; set; }
        public DbSet<Identity> Identity { get; set; }

        public StormhammerContext(DbContextOptions<StormhammerContext> options)
    : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:stormhammer.database.windows.net,1433;Initial Catalog=stormhammer;Persist Security Info=False;User ID=stormhammer;Password=C088mn9T;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MobClassConfiguration());
            modelBuilder.ApplyConfiguration(new MobRaceConfiguration());
            modelBuilder.ApplyConfiguration(new MobConfiguration());
            modelBuilder.ApplyConfiguration(new IdentityConfiguration());
        }
    }
}