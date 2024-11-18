using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.BreedManagement.Application.Database;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Queries.GetBreedById;

public class GetBreedByIdHandler
    :IQueryHandler<BreedDto, GetBreedByIdQuery>
{
    private readonly IReadDbContext _context;
    private readonly ILogger<GetBreedByIdHandler> _logger;

    public GetBreedByIdHandler(
        IReadDbContext context,
        ILogger<GetBreedByIdHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<BreedDto>> HandleAsync(
        GetBreedByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var breed = await _context.Breeds
            .FirstOrDefaultAsync(x => x.Id == query.BreedId, cancellationToken: cancellationToken);

        if (breed is null)
            return Errors.General.NotFound($"Breed with id: {query.BreedId} does not exist").ToErrorList();

        return breed;
    }
}