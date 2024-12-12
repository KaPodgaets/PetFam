using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Infrastructure.DbContexts;

public class ApplicationsWriteDbContext(
    string connectionString) : DbContext
{
    public DbSet<VolunteeringApplication> Applications => Set<VolunteeringApplication>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(connectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        modelBuilder.HasDefaultSchema("applications");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}