using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Infrastructure.DbContexts;
using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Infrastructure.Migrator;

public class AccountsMigrator(
    DiscussionsWriteDbContext context,
    ILogger<AccountsMigrator> logger)
    : IMigrator
{
    public async Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        logger.Log(LogLevel.Information, "Applying discussion migrations...");
        
        if (await context.Database.CanConnectAsync(cancellationToken) is false)
        {
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
        
        await context.Database.MigrateAsync(cancellationToken);
        
        logger.Log(LogLevel.Information, "Migrations discussion applied successfully.");
    }
}