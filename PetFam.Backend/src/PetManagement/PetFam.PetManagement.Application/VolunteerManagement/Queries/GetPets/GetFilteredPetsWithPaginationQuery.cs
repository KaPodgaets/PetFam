﻿using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Queries.GetPets
{
    public record GetFilteredPetsWithPaginationQuery(
        Guid? VolunteerId,
        Guid? SpeciesId,
        Guid? BreedId,
        string? Nickname,
        int? AgeFrom,
        int? AgeTo,
        string? Color,
        string? Country,        
        int? PositionFrom,
        int? PositionTo,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize) : IQuery;
}