using CrealutionRealtimeServer.Domain.Entities;
using CrealutionRealtimeServer.Configurations.Database.EntityConfigurations.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CrealutionRealtimeServer.Configurations.Database.EntityConfigurations
{
    public class AccountConfiguration : IEntityConfiguration
    {
        public void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Account>()
                .Property(p => p.Name)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(p => p.DisplayName)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .Property(p => p.Password)
                .HasMaxLength(255)
                .IsRequired();

            modelBuilder.Entity<Account>()
                .HasOne(p => p.Role)
                .WithMany(p => p.Accounts)
                .HasForeignKey(k => k.RoleId);
        }
    }
}