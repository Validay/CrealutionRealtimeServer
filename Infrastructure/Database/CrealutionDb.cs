using CrealutionRealtimeServer.Domain.Entities;
using CrealutionRealtimeServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace CrealutionRealtimeServer.Infrastructure.Database
{
    public class CrealutionDb : DbContext
    {
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Account> ConnectedAccounts { get; set; }

        protected CrealutionDb()
        {
        }

        public CrealutionDb(DbContextOptions<CrealutionDb> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var configurations = Assembly.GetAssembly(typeof(IEntityConfiguration))
                .GetTypes()
                .Where(type => typeof(IEntityConfiguration).IsAssignableFrom(type)
                    && !type.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEntityConfiguration>();

            foreach (var configuration in configurations)
                configuration.Configure(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}