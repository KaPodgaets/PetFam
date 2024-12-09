using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.VolunteeringApplications.Infrastructure.DbContexts;

namespace PetFam.VolunteeringApplications.Infrastructure.Migrator;

public class ApplicationsMigrator(
    ApplicationsWriteDbContext context,
    ILogger<ApplicationsMigrator> logger)
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