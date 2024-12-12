using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Infrastructure.Migrator;

public class VolunteersMigrator(
    VolunteersWriteDbContext context,
    ILogger<VolunteersMigrator> logger)
    : IMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Applying accounts migrations...");
        
        if (await context.Database.CanConnectAsync(cancellationToken) is false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
        
        await context.Database.MigrateAsync(cancellationToken);
        
        logger.Log(LogLevel.Information, "Migrations accounts applied successfully.");
    }
}