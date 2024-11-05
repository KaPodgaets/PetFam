using PetFam.Application.Database;
using PetFam.Application.Dtos;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetAllPets
{
    public class GetAllPetsWithPaginationHandler
        : IQueryHandler<PagedList<PetDto>, GetPetsWithPaginationQuery>
    {
        private readonly IReadDbContext _dbContext;
        public GetAllPetsWithPaginationHandler(IReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<PetDto>> HandleAsync(
            GetPetsWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var source = _dbContext.Pets.AsQueryable();

            // filtration

            var pagedList = await source.ToPagedList(query.PageNumber, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
