using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Shared;

namespace PetFam.Accounts.Infrastructure.Seeding;

public class AccountsSeeder
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AccountsSeeder> _logger;


    public AccountsSeeder(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<AccountsSeeder> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task SeedAsync(CancellationToken stoppingToken = default)
    {
        _logger.LogInformation("Seeding accounts...");
        
        var json = await File.ReadAllTextAsync(ConfigurationJsonFilesPaths.Permissions);

        using var scope = _serviceScopeFactory.CreateScope();
        var accountsContext = scope.ServiceProvider.GetRequiredService<AccountsWriteDbContext>();
        
        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
            ?? throw new ApplicationException("roles config json could not be deserialized.");
        
        var permissionsToAdd = seedData.Permissions.SelectMany(permissionGroup => permissionGroup.Value);
        var rolesToAdd = seedData.Roles.SelectMany(role => role.Value).ToList();
        
        var permissionManager = new PermissionManager(accountsContext);
        await permissionManager.AddIfNotExist(permissionsToAdd);
        
        _logger.LogInformation("Added {count} permissions", permissionsToAdd.Count());
        
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    }
}