using Microsoft.EntityFrameworkCore;
using PetFam.Application.Volunteers;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Infrastructure.Repositories
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public VolunteerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Add(Volunteer model, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Volunteers.AddAsync(model, cancellationToken);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return $"{ex.Message}";
            }

            return model.Id.Value;
        }

        public async Task<Result<Volunteer>> GetById(VolunteerId id)
        {
            var model = await _dbContext.Volunteers
                .Include(m => m.Pets)
                .FirstOrDefaultAsync(x => x.Id == id);


            if (model == null)
            {
                return $"There is not volunteer with id {id.Value}";
            }

            return model;
        }
    }
}
