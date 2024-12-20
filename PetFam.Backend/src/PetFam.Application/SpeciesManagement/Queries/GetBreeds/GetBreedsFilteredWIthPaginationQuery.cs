using PetFam.Application.Interfaces;

namespace PetFam.Application.SpeciesManagement.Queries.GetBreeds;

public record GetBreedsFilteredWIthPaginationQuery(
    Guid SpeciesId,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize):IQuery;