using PetFam.PetManagement.Application.Database;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos;
using PetFam.Shared.Extensions;
using PetFam.Shared.Models;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.PetManagement.Application.VolunteerManagement.Queries.GetAllVolunteers
{
    public class GetVolunteersWithPaginationHandler
        : IQueryHandler<PagedList<VolunteerDto>, GetVolunteersWithPaginationQuery>
    {
        private readonly IReadDbContext _dbContext;
        public GetVolunteersWithPaginationHandler(IReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<PagedList<VolunteerDto>>> HandleAsync(
            GetVolunteersWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var volunteersQuery = _dbContext.Volunteers.AsQueryable();

            volunteersQuery = volunteersQuery.WhereIf(
                query.VolunteerId is not null, p => p.Id == query.VolunteerId);

            var pagedList = await volunteersQuery
                .ToPagedList(query.PageNumber, query.PageSize, cancellationToken);

            return pagedList;
        }
    }
}
