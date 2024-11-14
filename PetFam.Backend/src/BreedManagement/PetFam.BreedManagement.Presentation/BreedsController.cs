using PetFam.BreedManagement.Contracts.BreedsRequests;

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