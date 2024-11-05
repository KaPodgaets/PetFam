using Microsoft.EntityFrameworkCore;
using PetFam.Application.Database;
using PetFam.Application.Dtos;

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

        public async Task<IReadOnlyList<VolunteerDto>> Handle(
            GetVolunteersWithPaginationQuery query,
            CancellationToken cancellationToken = default)
        {
            var offset = query.PageSize * (query.PageNumber - 1);
            var volunteers = await _dbContext.Volunteers
                .Skip(offset)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);

            return volunteers;
        }
    }
}
