using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.IdentityManagers;
using PetFam.Shared;

namespace PetFam.Accounts.Infrastructure.Seeding;

public class AccountsSeeder(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<AccountsSeeder> logger)
{
    private readonly ILogger<AccountsSeeder> _logger = logger;

    public async Task SeedAsync(CancellationToken stoppingToken = default)
    {
        using var scope = serviceScopeFactory.CreateScope();

        var seedingService = scope.ServiceProvider.GetRequiredService<AccountsSeedingService>();
        await seedingService.SeedAsync(stoppingToken);
    }
}