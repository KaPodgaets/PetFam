using System.Linq.Expressions;
using PetFam.Volunteers.Application.Database;

namespace PetFam.Volunteers.Application.VolunteerManagement.Queries.GetPets
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
                "volunteer" => (pet) => pet.VolunteerId,
                "age" => (pet) => pet.HealthInfo.Age,
                "breed" => (pet) => pet.SpeciesAndBreed.BreedId,
                "color" => (pet) => pet.GeneralInfo.Color,
                "country" => (pet) => pet.Address.Country,
                _ => (pet) => pet.Order
            };

            petsQuery = query.SortDirection?.ToLower() == "desc"
            ? petsQuery.OrderByDescending(keySelector)
            : petsQuery.OrderBy(keySelector);


            petsQuery = petsQuery
                .WhereIf(query.VolunteerId is not null, p=> p.VolunteerId == query.VolunteerId)
                .WhereIf(
                    query.SpeciesId is not null, p => p.SpeciesAndBreed.SpeciesId == query.SpeciesId)
                .WhereIf(query.BreedId is not null, p => p.SpeciesAndBreed.BreedId == query.BreedId)
                .WhereIf(query.Nickname is not null, p => p.NickName == query.Nickname)
                .WhereIf(query.AgeFrom is not null, p=> p.HealthInfo.Age >= query.AgeFrom)
                .WhereIf(query.AgeTo is not null, p => p.HealthInfo.Age <= query.AgeTo)
                .WhereIf(query.Color is not null, p => p.GeneralInfo.Color == query.Color)
                .WhereIf(query.Country is not null, p=> p.Address.Country == query.Country)
                .WhereIf(query.PositionFrom is not null, p => p.Order >= query.PositionFrom)
                .WhereIf(query.PositionTo is not null, p => p.Order <= query.PositionTo);

            var pagedList = await petsQuery
                .ToPagedList(query.Page, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
