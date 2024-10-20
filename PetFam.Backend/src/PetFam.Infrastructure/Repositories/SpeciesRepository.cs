using Microsoft.EntityFrameworkCore;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer;

namespace PetFam.Infrastructure.Repositories
{
    public class SpeciesRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SpeciesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Guid>> Add(Species model, CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.Species.AddAsync(model, cancellationToken);

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return Errors.General.Failure();
            }

            return model.Id.Value;
        }

        public async Task<Result<Species>> GetById(SpeciesId id, CancellationToken cancellationToken = default)
        {
            var model = await _dbContext.Species
                .Include(m => m.Breeds)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);


            if (model == null)
            {
                return Errors.General.NotFound(id.Value);
            }

            return model;
        }

        public async Task<Result<Guid>> Update(Species species, CancellationToken cancellationToken = default)
        {
            _dbContext.Species.Attach(species);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return species.Id.Value;
        }

        public async Task<Result<Guid>> Delete(Species species, CancellationToken cancellationToken = default)
        {
            _dbContext.Species.Remove(species);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return species.Id.Value;
        }
    }
}
