using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Domain;

namespace PetFam.Discussions.Infrastructure.DbContexts;

public class DiscussionsWriteDbContext(
    string connectionString) : DbContext
{
    public DbSet<Discussion> Discussions => Set<Discussion>();
    public DbSet<Message> Messages => Set<Message>();

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
            typeof(DiscussionsWriteDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Write") ?? false);

        modelBuilder.HasDefaultSchema("applications");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}