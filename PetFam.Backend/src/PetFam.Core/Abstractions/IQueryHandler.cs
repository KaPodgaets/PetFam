namespace PetFam.Shared.Abstructions
{
    public interface IQueryHandler<TValue, in TQuery> where TQuery : IQuery
    {
        Task<Result<TValue>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
