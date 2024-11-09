using PetFam.Application.Database;
using PetFam.Application.Dtos;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetAllPets
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

            petsQuerry = petsQuerry.WhereIf(
                query.SpeciesId is not null, p => p.SpeciesAndBreed.SpeciesId == query.SpeciesId)
                .WhereIf(query.PositionFrom is not null, p => p.Order >= query.PositionFrom)
                .WhereIf(query.PositionTo is not null, p => p.Order <= query.PositionTo);


            if(query.PositionFrom is not null)
            {
                petsQuerry = petsQuerry
                    .Where(p => p.Order >= query.PositionFrom).AsQueryable();
            }

            if (query.PositionTo is not null)
            {
                petsQuerry = petsQuerry
                    .Where(p => p.Order <= query.PositionTo).AsQueryable();
            }

            var pagedList = await petsQuerry
                .OrderBy(p => p.Order)
                .ToPagedList(query.Page, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
