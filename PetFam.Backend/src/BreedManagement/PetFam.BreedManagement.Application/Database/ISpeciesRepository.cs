using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Application.Database
{
    public interface ISpeciesRepository
    {
        Task<Result<Guid>> Add(BreedManagement.Domain.Species model, CancellationToken cancellationToken = default);
        Task<Result<Guid>> Delete(BreedManagement.Domain.Species species, CancellationToken cancellationToken = default);
        Task<Result<BreedManagement.Domain.Species>> GetById(SpeciesId id, CancellationToken cancellationToken = default);
        Task<Result<BreedManagement.Domain.Species>> GetByName(string name, CancellationToken cancellationToken = default);
        Task<Result<Guid>> Update(BreedManagement.Domain.Species species, CancellationToken cancellationToken = default);
    }
}