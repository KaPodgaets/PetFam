using PetFam.Domain.Shared;

namespace PetFam.Application.Interfaces
{
    public interface IQueryHandler<TValue, in TQuery> where TQuery : IQuery
    {
        Task<Result<TValue>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
