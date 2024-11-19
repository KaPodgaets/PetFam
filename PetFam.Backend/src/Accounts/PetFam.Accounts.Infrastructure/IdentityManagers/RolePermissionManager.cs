using Microsoft.EntityFrameworkCore;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure.IdentityManagers;

public class RolePermissionManager(
    AccountsWriteDbContext accountsContext)
{
    public async Task CreateRangeAsync(
        IEnumerable<RolePermission> rolePermissions,
        CancellationToken stoppingToken = default)
    {
        foreach (var rolePermission in rolePermissions)
        {
            var isRolePermissionExists = await accountsContext.RolePermissions
                .AnyAsync(
                    rp => rp.RoleId == rolePermission.RoleId && rp.PermissionId == rolePermission.PermissionId,
                    cancellationToken: stoppingToken);
            
            if (isRolePermissionExists)
                continue;
        
            await accountsContext.RolePermissions.AddAsync(rolePermission, stoppingToken);
        }
        
        await accountsContext.SaveChangesAsync(stoppingToken);
    }
}