using Microsoft.EntityFrameworkCore;
using PetFam.BreedManagement.Application.Database;
using PetFam.BreedManagement.Domain;
using PetFam.BreedManagement.Domain.Entities;
using PetFam.BreedManagement.Infrastructure.Contexts;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Infrastructure
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly WriteDbContext _dbContext;
        public SpeciesRepository(WriteDbContext dbContext)
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
                return Errors.General.Failure().ToErrorList();
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
                return Errors.General.NotFound(id.Value).ToErrorList();
            }

            return model;
        }

        public async Task<Result<Species>> GetByName(string name, CancellationToken cancellationToken = default)
        {
            var model = await _dbContext.Species
                .Include(m => m.Breeds)
                .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);


            if (model == null)
            {
                return Errors.General.NotFound(name).ToErrorList();
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

        public Result<bool> CheckSpeciesExists(string name, CancellationToken cancellationToken = default)
        {
             return _dbContext.Species.Any(x => x.Name == name);
        }

        public async Task<Result<bool>> CheckBreedExists(string speciesName, string breedName, CancellationToken cancellationToken = default)
        {
            var model = await _dbContext.Species
                .Include(m => m.Breeds)
                .FirstOrDefaultAsync(x => x.Name == speciesName, cancellationToken);

            if (model == null)
            {
                return false;
            }

            List<Breed> breeds = (List<Breed>)model.Breeds;

            return breeds.Any(x => x.Name == breedName);
        }
    }
}
