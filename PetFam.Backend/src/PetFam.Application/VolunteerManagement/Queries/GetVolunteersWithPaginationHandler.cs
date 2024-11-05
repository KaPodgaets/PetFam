using PetFam.Application.Database;
using PetFam.Application.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Threading;
using PetFam.Application.Extensions;

namespace PetFam.Application.VolunteerManagement.Queries
{
    public record GetVolunteersWithPaginationQuery(int PageNumber, int PageSize); 
    public class GetVolunteersWithPaginationHandler
    {
        private readonly IReadDbContext _dbContext;
        public GetVolunteersWithPaginationHandler(IReadDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedList<VolunteerDto>> Handle(
            GetVolunteersWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var volunteerQuery = _dbContext.Volunteers.AsQueryable();

            // filtration

            var pagedList = await volunteerQuery.ToPagedList(query.PageNumber, query.PageSize, cancellationToken);
            
            return pagedList;
        }
    }
}
