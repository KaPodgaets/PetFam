using PetFam.Shared.Abstractions;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Queries.Get;

public record GetSpeciesFilteredWithPaginationQuery(
    int? PositionFrom,
    int? PositionTo,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize) : IQuery;