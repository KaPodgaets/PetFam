using PetFam.Shared.Shared;

namespace PetFam.Shared.Abstractions
{
    public interface IQueryHandler<TValue, in TQuery> where TQuery : IQuery
    {
        Task<Result<TValue>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
