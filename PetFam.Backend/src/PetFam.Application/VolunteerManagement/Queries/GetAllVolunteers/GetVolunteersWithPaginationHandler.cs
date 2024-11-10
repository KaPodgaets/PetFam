using PetFam.Application.Database;
using PetFam.Application.Dtos;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetAllVolunteers
{
    public class GetVolunteersWithPaginationHandler
        : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
    {
        private readonly IReadDbContext _dbContext;
        public GetVolunteersWithPaginationHandler(IReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<VolunteerDto>> HandleAsync(
            GetVolunteersWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var source = _dbContext.Volunteers.AsQueryable();

            // filtration

            var pagedList = await source.ToPagedList(query.PageNumber, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
