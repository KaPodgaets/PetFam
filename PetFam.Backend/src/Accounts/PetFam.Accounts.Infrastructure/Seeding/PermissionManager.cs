using Microsoft.EntityFrameworkCore;
using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Infrastructure.Seeding;

public class PermissionManager(AccountsWriteDbContext accountsContext, CancellationToken stoppingToken = default)
{
    public async Task AddIfNotExist(IEnumerable<string> permissionCodes)
    {
        foreach (var permissionCode in permissionCodes)
        {
            var isPermissionExist = await accountsContext.Permissions
                .AnyAsync(permission => permission.Code == permissionCode);
        
            if (isPermissionExist)
                return;
        
            await accountsContext.Permissions.AddAsync(new Permission(Guid.NewGuid(), permissionCode));
        }
        
        await accountsContext.SaveChangesAsync(stoppingToken);
        
    }
    
    
}