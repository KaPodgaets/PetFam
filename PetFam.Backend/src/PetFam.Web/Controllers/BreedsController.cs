using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Requests.Breed;
using PetFam.Application;
using PetFam.Application.Dtos;
using PetFam.Application.SpeciesManagement.Queries.GetBreeds;

namespace PetFam.Api.Controllers;

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