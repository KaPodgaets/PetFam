using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Dtos;
using PetFam.Shared.Options;
using PetFam.PetManagement.Application.Database;

namespace PetFam.PetManagement.Infrastructure.DbContexts
{
    public class ReadDbContext(
        string connectionString) : DbContext, IReadDbContext
    {
        public DbSet<VolunteerDto> Volunteers { get; set; }
        public DbSet<PetDto> Pets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
            optionsBuilder.UseSnakeCaseNamingConvention();
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ReadDbContext).Assembly,
                type => type.FullName?.Contains("Configurations.Read") ?? false);
        }

        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
    }
}
