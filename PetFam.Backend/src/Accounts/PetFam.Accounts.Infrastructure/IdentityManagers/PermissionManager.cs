using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure.IdentityManagers;

public class PermissionManager(
    AccountsWriteDbContext accountsContext)
{
    public async Task AddIfNotExist(
        IEnumerable<string> permissionCodes,
        CancellationToken stoppingToken = default)
    {
        foreach (var permissionCode in permissionCodes)
        {
            var isPermissionExist = await accountsContext.Permissions
                .AnyAsync(
                    permission => permission.Code == permissionCode,
                    cancellationToken: stoppingToken);
        
            if (isPermissionExist)
                return;
        
            await accountsContext.Permissions
                .AddAsync(new Permission(Guid.NewGuid(), permissionCode), stoppingToken);
        }
        
        await accountsContext.SaveChangesAsync(stoppingToken);
    }

    public async Task<Permission?> FindByCode(string permissionCode, CancellationToken stoppingToken = default)
    {
        var permission = await accountsContext.Permissions
            .FirstOrDefaultAsync(permission => permission.Code == permissionCode, stoppingToken);
        
        return permission;
    }

    public async Task<IEnumerable<string>> GetUserPermissionsCode(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var permissions = await accountsContext.Users
            .Include(u => u.Roles)
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Roles)
            .SelectMany(r => r.RolePermissions)
            .Select(rp => rp.Permission.Code)
            .ToListAsync(cancellationToken);
        
        return permissions;
    }
}