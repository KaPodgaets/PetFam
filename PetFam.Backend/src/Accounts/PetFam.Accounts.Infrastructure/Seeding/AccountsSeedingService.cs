using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.IdentityManagers;
using PetFam.Accounts.Infrastructure.Options;
using PetFam.Shared;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel;

namespace PetFam.Accounts.Infrastructure.Seeding;

public class AccountsSeedingService
{
    private readonly ILogger<AccountsSeedingService> _logger;
    private readonly UserManager<User> _userManager;
    private readonly AdminOptions _adminOptions;
    private readonly RoleManager<Role> _roleManager;
    private readonly RolePermissionManager _rolePermissionManager;
    private readonly PermissionManager _permissionManager;
    private readonly AdminAccountsManager _adminAccountsManager;
    private readonly IUnitOfWork _unitOfWork;

    public AccountsSeedingService(
        ILogger<AccountsSeedingService> logger,
        UserManager<User> userManager,
        IOptions<AdminOptions> adminOptions,
        RoleManager<Role> roleManager,
        RolePermissionManager rolePermissionManager,
        PermissionManager permissionManager, 
        AdminAccountsManager adminAccountsManager, 
        [FromKeyedServices(Modules.Accounts)] IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _userManager = userManager;
        _adminOptions = adminOptions.Value;
        _roleManager = roleManager;
        _rolePermissionManager = rolePermissionManager;
        _permissionManager = permissionManager;
        _adminAccountsManager = adminAccountsManager;
        _unitOfWork = unitOfWork;
    }

    public async Task SeedAsync(CancellationToken stoppingToken = default)
    {
        var seedData = await ReadRolePermissionConfig();

        _logger.LogInformation("Seeding accounts...");

        await SeedPermissions(seedData, stoppingToken);
        await SeedRoles(seedData, _roleManager);
        await SeedRolePermissions(seedData, stoppingToken);
        await SeedAdminUser(stoppingToken);
        
        _logger.LogInformation("Stop seeding accounts");
    }

    private async Task SeedAdminUser(CancellationToken stoppingToken = default)
    {
        var existingAdmin = _userManager.FindByEmailAsync(_adminOptions.Email).Result;
        if (existingAdmin is not null)
            return; //no need to seed
        
        var adminRole = await _roleManager.FindByNameAsync(AdminOptions.RoleName)
                        ?? throw new ApplicationException("Could not find admin role.");
        
        var adminUser = User.CreateAdmin(_adminOptions.Email, [adminRole]);

        var transaction = await _unitOfWork.BeginTransaction(stoppingToken);
        
        _logger.LogInformation("begin transaction to seed admin user and admin account");
        
        // TODO: how to add adminAccountId into record of user in DB?
        try
        {
            var result = await _userManager.CreateAsync(adminUser, _adminOptions.Password);
            if(result.Succeeded is false)
                throw new ApplicationException("Could not create admin user.");
        
            var adminAccount = new AdminAccount
            {
                UserId = adminUser.Id,
                FullName = _adminOptions.UserName,
            };
            await _adminAccountsManager.CreateAccount(adminAccount, stoppingToken);

            adminUser.AdminAccount = adminAccount;
            await _userManager.UpdateAsync(adminUser);
            
            transaction.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError("Seeding admin was failed with error: {error}", e.Message);
            transaction.Rollback();
            throw new ApplicationException(e.Message);
        }
        _logger.LogInformation("successfully end transaction to seed admin user and admin account");
    }

    private async Task SeedRolePermissions(
        RolePermissionOptions seedData,
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

    private async Task<RolePermissionOptions> ReadRolePermissionConfig()
    {
        var json = await File.ReadAllTextAsync(ConfigurationJsonFilesPaths.Permissions);
        var seedData = JsonSerializer.Deserialize<RolePermissionOptions>(json)
                       ?? throw new ApplicationException("roles config json could not be deserialized.");
        return seedData;
    }

    private async Task SeedPermissions(
        RolePermissionOptions seedData,
        CancellationToken stoppingToken = default)
    {
        var permissionsToAdd = seedData.Permissions
            .SelectMany(permissionGroup => permissionGroup.Value)
            .ToList();

        await _permissionManager.AddIfNotExist(permissionsToAdd, stoppingToken);

        _logger.LogInformation("Added {count} permissions", permissionsToAdd.Count);
    }

    private async Task SeedRoles(
        RolePermissionOptions seedData,
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