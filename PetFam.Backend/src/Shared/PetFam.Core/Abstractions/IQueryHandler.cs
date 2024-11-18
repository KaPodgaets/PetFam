using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Shared.Abstractions
{
    public interface IQueryHandler<TValue, in TQuery> where TQuery : IQuery
    {
        Task<Result<TValue>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
