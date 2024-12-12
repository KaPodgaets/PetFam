using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PetFam.Shared.Abstractions;
using PetFam.VolunteeringApplications.Infrastructure.DbContexts;

namespace PetFam.VolunteeringApplications.Infrastructure;

public class UnitOfWork(ApplicationsWriteDbContext context)
    : IUnitOfWork
{
    
    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        return transaction.GetDbTransaction();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}