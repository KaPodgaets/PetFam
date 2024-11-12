using PetFam.Application.Database;
using PetFam.Application.Dtos;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;
using PetFam.Application.VolunteerManagement.PetManagement.AddPhotos;
using System.Linq.Expressions;

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

        public async Task<PagedList<PetDto>> HandleAsync(
            GetFilteredPetsWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var petsQuerry = _dbContext.Pets.AsQueryable();

            Expression<Func<PetDto, object>> keySelector = query.SortBy?.ToLower() switch
            {
                "nickname" => (pet) => pet.NickName,
                _ => (pet) => pet.Order
            };


            petsQuerry = petsQuerry.WhereIf(
                query.SpeciesId is not null, p => p.SpeciesAndBreed.SpeciesId == query.SpeciesId)
                .WhereIf(query.PositionFrom is not null, p => p.Order >= query.PositionFrom)
                .WhereIf(query.PositionTo is not null, p => p.Order <= query.PositionTo);


            var pagedList = await petsQuerry
                .ToPagedList(query.Page, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
