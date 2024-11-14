using System.Linq.Expressions;

namespace PetFam.Species.Application.SpeciesManagement.Queries.GetBreeds;

public class GetBreedsFilteredWithPaginationHandler
    :IQueryHandler<PagedList<BreedDto>, GetBreedsFilteredWIthPaginationQuery>
{
    private readonly IReadDbContext _dbContext;
    private readonly IValidator<GetBreedsFilteredWIthPaginationQuery> _validator;

    public GetBreedsFilteredWithPaginationHandler(
        IReadDbContext dbContext,
        IValidator<GetBreedsFilteredWIthPaginationQuery> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }
    
    public async Task<Result<PagedList<BreedDto>>> HandleAsync(
        GetBreedsFilteredWIthPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var breedQuery = _dbContext.Breeds.AsQueryable();

        Expression<Func<BreedDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (breed) => breed.Name,
            _ => (breed) => breed.Name
        };

        breedQuery = query.SortDirection?.ToLower() == "desc"
            ? breedQuery.OrderByDescending(keySelector)
            : breedQuery.OrderBy(keySelector);
        
        breedQuery = breedQuery
            .Where(b => b.SpeciesId == query.SpeciesId);

        var pagedList = await breedQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;
    }
}