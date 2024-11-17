using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.PetManagement.Application.Database;
using PetFam.PetManagement.Contracts;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

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
    public Task<Result<bool>> IsPetsWithBreedExisting(BreedId breedId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> IsPetsWithSpeciesExisting(SpeciesId speciesId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<string>> GetAllPhotoPaths(CancellationToken cancellationToken = default)
    {
        var paths = await _readDbContext.Pets
            .SelectMany(p => p.Photos)
            .Select(photo => photo.Filepath)
            .ToListAsync(cancellationToken);
        
        return paths;
    }
}