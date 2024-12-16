using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFam.Discussions.Infrastructure.DbContexts;
using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Infrastructure;

public class UnitOfWork(DiscussionsWriteDbContext dbContext)
    : IUnitOfWork
{
    
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}