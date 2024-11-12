using PetFam.Application.Database;
using System.Linq.Expressions;
using PetFam.Application.Dtos;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;
using PetFam.Domain.Shared;

namespace PetFam.Application.SpeciesManagement.Queries.Get;

public class GetSpeciesFilteredWithPaginationHandler 
    :IQueryHandler<PagedList<SpeciesDto>,GetSpeciesFilteredWithPaginationQuery>
{
    private readonly IReadDbContext _dbContext;
    public GetSpeciesFilteredWithPaginationHandler(IReadDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<PagedList<SpeciesDto>>> HandleAsync(
        GetSpeciesFilteredWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var speciesQuery = _dbContext.Species.AsQueryable();

        Expression<Func<SpeciesDto, object>> keySelector = query.SortBy?.ToLower() switch
        {
            "name" => (species) => species.Name,
            _ => (species) => species.Name
        };

        speciesQuery = query.SortDirection?.ToLower() == "desc"
            ? speciesQuery.OrderByDescending(keySelector)
            : speciesQuery.OrderBy(keySelector);


        var pagedList = await speciesQuery
            .ToPagedList(query.Page, query.PageSize, cancellationToken);

        return pagedList;
    }
}