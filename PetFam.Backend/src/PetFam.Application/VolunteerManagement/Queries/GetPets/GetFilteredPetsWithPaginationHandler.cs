using PetFam.Application.Database;
using PetFam.Application.Dtos;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;
using System.Linq.Expressions;
using PetFam.Domain.Shared;

namespace PetFam.Application.VolunteerManagement.Queries.GetPets
{
    public class GetFilteredPetsWithPaginationHandler
        : IQueryHandler<PagedList<PetDto>, GetFilteredPetsWithPaginationQuery>
    {
        private readonly IReadDbContext _dbContext;
        public GetFilteredPetsWithPaginationHandler(IReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagedList<PetDto>>> HandleAsync(
            GetFilteredPetsWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var petsQuery = _dbContext.Pets.AsQueryable();

            Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
            {
                "nickname" => (pet) => pet.NickName,
                _ => (pet) => pet.Order
            };

            petsQuery = query.SortDirection?.ToLower() == "desc"
            ? petsQuery.OrderByDescending(keySelector)
            : petsQuery.OrderBy(keySelector);


            petsQuery = petsQuery.WhereIf(
                query.SpeciesId is not null, p => p.SpeciesAndBreed.SpeciesId == query.SpeciesId)
                .WhereIf(query.PositionFrom is not null, p => p.Order >= query.PositionFrom)
                .WhereIf(query.PositionTo is not null, p => p.Order <= query.PositionTo);


            var pagedList = await petsQuery
                .ToPagedList(query.Page, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
