using Microsoft.EntityFrameworkCore;
using PetFam.Accounts.Application.Database;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Infrastructure.IdentityManagers;

public class RefreshSessionsManager(AccountsWriteDbContext accountsContext) : IRefreshSessionsManager
{
    public async Task<Result<RefreshSession>> GetByRefreshToken(
        Guid refreshToken,
        CancellationToken cancellationToken = default)
    {
        var refreshSession = await accountsContext.RefreshSessions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.RefreshToken == refreshToken, cancellationToken);
        
        if (refreshSession is null)
            return Errors.General.Failure().ToErrorList();

        return refreshSession;
    }
    
    public async Task DeleteById(
        RefreshSession session,
        CancellationToken cancellationToken = default)
    {
        accountsContext.RefreshSessions
            .Remove(session);
        
        await accountsContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task Create(
        RefreshSession session,
        CancellationToken cancellationToken = default)
    {
        accountsContext.RefreshSessions
            .Add(session);
        
        await accountsContext.SaveChangesAsync(cancellationToken);
    }
}