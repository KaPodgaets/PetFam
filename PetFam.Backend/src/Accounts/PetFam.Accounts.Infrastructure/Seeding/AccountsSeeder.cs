using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.IdentityManagers;
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
        using var scope = _serviceScopeFactory.CreateScope();
        var accountsContext = scope.ServiceProvider.GetRequiredService<AccountsWriteDbContext>();

        var permissionManager = new PermissionManager(accountsContext);
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
        var rolePermissionManager = scope.ServiceProvider.GetRequiredService<RolePermissionManager>();

        var seedData = await ReadRolePermissionConfig();

        _logger.LogInformation("Seeding accounts...");

        await SeedPermissions(seedData, permissionManager, stoppingToken);
        await SeedRoles(seedData, roleManager);
        await SeedRolePermissions(
            seedData,
            roleManager,
            permissionManager,
            rolePermissionManager,
            stoppingToken);
    }

    private static async Task SeedRolePermissions(
        RolePermissionConfig seedData,
        RoleManager<Role> roleManager,
        PermissionManager permissionManager,
        RolePermissionManager rolePermissionManager,
        CancellationToken stoppingToken)
    {
        List<RolePermission> rolePermissions = [];
        
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if(role is null)
                throw new ApplicationException($"Role {roleName} not found during seeding");

            foreach (var permissionCode in seedData.Roles[roleName])
            {
                var permission = await permissionManager.FindByCode(permissionCode, stoppingToken);
                if(permission is null)
                    throw new KeyNotFoundException("permission not found during seeding");
                
                rolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                });
            }
            await rolePermissionManager.CreateRangeAsync(rolePermissions, stoppingToken);
        }
    }

    private static async Task<RolePermissionConfig> ReadRolePermissionConfig()
    {
        var json = await File.ReadAllTextAsync(ConfigurationJsonFilesPaths.Permissions);
        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
                       ?? throw new ApplicationException("roles config json could not be deserialized.");
        return seedData;
    }

    private async Task SeedPermissions(
        RolePermissionConfig seedData,
        PermissionManager permissionManager,
        CancellationToken stoppingToken = default)
    {
        var permissionsToAdd = seedData.Permissions
            .SelectMany(permissionGroup => permissionGroup.Value)
            .ToList();

        await permissionManager.AddIfNotExist(permissionsToAdd, stoppingToken);

        _logger.LogInformation("Added {count} permissions", permissionsToAdd.Count());
    }

    private async Task SeedRoles(
        RolePermissionConfig seedData,
        RoleManager<Role> roleManager)
    {
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role is null)
            {
                await roleManager.CreateAsync(new Role { Name = roleName });
            }
        }

        _logger.LogInformation("Roles added to database.");
    }
}