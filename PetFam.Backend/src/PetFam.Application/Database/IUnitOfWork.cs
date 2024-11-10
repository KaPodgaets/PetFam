using System.Data;

namespace PetFam.Application.Database
{
    public interface IUnitOfWork
    {
        Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}