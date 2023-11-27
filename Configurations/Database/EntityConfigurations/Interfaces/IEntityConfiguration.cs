using Microsoft.EntityFrameworkCore;

namespace CrealutionRealtimeServer.Configurations.Database.EntityConfigurations.Interfaces
{
    public interface IEntityConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}