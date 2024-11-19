using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.IdentityManagers;
using PetFam.Shared;

namespace PetFam.Accounts.Infrastructure.Seeding;

public class AccountsSeedingService
{
    private readonly ILogger<AccountsSeedingService> _logger;
    private readonly UserManager<User> _userManager;
    private readonly AdminOptions _adminOptions;
    private readonly RoleManager<Role> _roleManager;
    private readonly RolePermissionManager _rolePermissionManager;
    private readonly PermissionManager _permissionManager;

    public AccountsSeedingService(
        ILogger<AccountsSeedingService> logger,
        UserManager<User> userManager,
        IOptions<AdminOptions> adminOptions,
        RoleManager<Role> roleManager,
        RolePermissionManager rolePermissionManager,
        PermissionManager permissionManager)
    {
        _logger = logger;
        _userManager = userManager;
        _adminOptions = adminOptions.Value;
        _roleManager = roleManager;
        _rolePermissionManager = rolePermissionManager;
        _permissionManager = permissionManager;
    }

    public async Task SeedAsync(CancellationToken stoppingToken = default)
    {
        var seedData = await ReadRolePermissionConfig();

        _logger.LogInformation("Seeding accounts...");

        await SeedPermissions(seedData, stoppingToken);
        await SeedRoles(seedData, _roleManager);
        await SeedRolePermissions(seedData, stoppingToken);

        var adminUser = new User
        {
            UserName = _adminOptions.UserName,
            Email = _adminOptions.Email
        };
        
        await _userManager.CreateAsync(adminUser, _adminOptions.Password);
        await _userManager.AddToRoleAsync(adminUser, "Admin");
    }
    
    private async Task SeedRolePermissions(
        RolePermissionConfig seedData,
        CancellationToken stoppingToken)
    {
        List<RolePermission> rolePermissions = [];
        
        foreach (var roleName in seedData.Roles.Keys)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if(role is null)
                throw new ApplicationException($"Role {roleName} not found during seeding");
            
            var rolePermissionsToAdd = seedData.Roles[roleName].ToList();
            foreach (var permissionCode in rolePermissionsToAdd)
            {
                var permission = await _permissionManager.FindByCode(permissionCode, stoppingToken);
                if(permission is null)
                    throw new KeyNotFoundException("permission not found during seeding");
                
                rolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                });
            }
        }
        await _rolePermissionManager.CreateRangeAsync(rolePermissions, stoppingToken);
    }

    private async Task<RolePermissionConfig> ReadRolePermissionConfig()
    {
        var json = await File.ReadAllTextAsync(ConfigurationJsonFilesPaths.Permissions);
        var seedData = JsonSerializer.Deserialize<RolePermissionConfig>(json)
                       ?? throw new ApplicationException("roles config json could not be deserialized.");
        return seedData;
    }

    private async Task SeedPermissions(
        RolePermissionConfig seedData,
        CancellationToken stoppingToken = default)
    {
        var permissionsToAdd = seedData.Permissions
            .SelectMany(permissionGroup => permissionGroup.Value)
            .ToList();

        await _permissionManager.AddIfNotExist(permissionsToAdd, stoppingToken);

        _logger.LogInformation("Added {count} permissions", permissionsToAdd.Count);
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