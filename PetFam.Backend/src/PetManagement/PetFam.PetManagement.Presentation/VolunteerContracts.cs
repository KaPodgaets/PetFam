using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.PetManagement.Application.Database;
using PetFam.PetManagement.Contracts;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.PetManagement.Presentation;

public class VolunteerContracts : IVolunteerContracts
{
    private readonly IReadDbContext _readDbContext;
    private readonly ILogger<VolunteerContracts> _logger;
    public VolunteerContracts(
        IReadDbContext readDbContext,
        ILogger<VolunteerContracts> logger)
    {
        _readDbContext = readDbContext;
        _logger = logger;
    }
    public async Task<Result<bool>> IsPetsWithBreedExisting(Guid breedId, CancellationToken cancellationToken = default)
    {
        var breeds = await _readDbContext.Pets
            .Select(p => p.SpeciesAndBreed.BreedId)
            .ToListAsync(cancellationToken);
        
        return breeds.Contains(breedId);
    }

    public async Task<Result<bool>> IsPetsWithSpeciesExisting(Guid speciesId, CancellationToken cancellationToken = default)
    {
        var breeds = await _readDbContext.Pets
            .Select(p => p.SpeciesAndBreed.SpeciesId)
            .ToListAsync(cancellationToken);
        
        return breeds.Contains(speciesId);
    }

    public async Task<IEnumerable<string>> GetAllPhotoPaths(CancellationToken cancellationToken = default)
    {
        var source = _readDbContext.Pets.AsAsyncEnumerable();
        var paths = new List<string>();

        await foreach (var pet in source.WithCancellation(cancellationToken))
        {
            paths.AddRange(pet.Photos.Select(photo => photo.Filepath));
        }
        
        return paths;
    }
}