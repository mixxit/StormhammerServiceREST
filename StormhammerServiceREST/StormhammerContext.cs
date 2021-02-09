using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StormhammerLibrary.Models;
using StormhammerServiceREST.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StormhammerServiceREST
{
    public class StormhammerContext : IdentityDbContext
    {
        public DbSet<MobClass> MobClass { get; set; }
        public DbSet<MobRace> MobRace { get; set; }
        public DbSet<Mob> Mob { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Zone> Zone { get; set; }

        private readonly DbContextOptions _options;
        public StormhammerContext(DbContextOptions<StormhammerContext> options)
    : base(options)
        {
            _options = options;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:stormhammer.database.windows.net,1433;Initial Catalog=stormhammer;Persist Security Info=False;User ID=stormhammer;Password=C088mn9T;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new MobClassConfiguration());
            modelBuilder.ApplyConfiguration(new MobRaceConfiguration());
            modelBuilder.ApplyConfiguration(new MobConfiguration());
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ZoneConfiguration());
        }

        internal bool AttemptRegisterUser(ClaimsPrincipal principal, Guid objectId, string email)
        {
            var identity = Account.FirstOrDefault(e => e.ObjectId.ToString().ToUpper().Equals(objectId.ToString().ToUpper()));
            if (identity == null)
            {
                identity = Account.Add(new Account() { ObjectId = objectId, Email = email }).Entity;
                SaveChanges();
            }

            return true;
        }
    }
}
