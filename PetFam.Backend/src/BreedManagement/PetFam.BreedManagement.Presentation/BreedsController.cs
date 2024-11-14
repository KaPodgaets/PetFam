using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.BreedManagement.Application.SpeciesManagement.Queries.GetBreeds;
using PetFam.BreedManagement.Presentation.Requests;
using PetFam.Framework;
using PetFam.Shared.Dtos;
using PetFam.Shared.Models;

namespace PetFam.BreedManagement.Presentation;

public class BreedsController:ApplicationController
{
    public BreedsController(ILogger<ApplicationController> logger) : base(logger)
    {
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<BreedDto>>> GetBreeds(
        [FromQuery] GetBreedsFilteredWithPaginationRequest request,
        [FromServices] GetBreedsFilteredWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        var result = await handler.HandleAsync(query, cancellationToken);
        
        return result.ToResponse();
    }
}