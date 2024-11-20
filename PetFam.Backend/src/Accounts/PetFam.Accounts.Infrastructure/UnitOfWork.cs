using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Infrastructure;

public class UnitOfWork(AccountsWriteDbContext accountsContext)
    : IUnitOfWork
{
    
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await accountsContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await accountsContext.SaveChangesAsync(cancellationToken);
    }
}