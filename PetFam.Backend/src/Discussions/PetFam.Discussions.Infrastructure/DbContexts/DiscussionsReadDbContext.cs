using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Application.Database;
using PetFam.Discussions.Domain;

namespace PetFam.Discussions.Infrastructure.DbContexts;

public class DiscussionsReadDbContext(
    string connectionString) : DbContext, IDiscussionsReadDbContext
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
            typeof(DiscussionsReadDbContext).Assembly,
            type => type.FullName?.Contains("Configurations.Read") ?? false);

        modelBuilder.HasDefaultSchema("discussions");
    }

    private ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });
}