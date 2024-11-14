using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Requests.Species;
using PetFam.Application;
using PetFam.Application.Dtos;
using PetFam.Application.SpeciesManagement.Commands.Create;
using PetFam.Application.SpeciesManagement.Commands.CreateBreed;
using PetFam.Application.SpeciesManagement.Commands.Delete;
using PetFam.Application.SpeciesManagement.Commands.DeleteBreed;
using PetFam.Application.SpeciesManagement.Queries.Get;

namespace PetFam.Api.Controllers
{
    public class SpeciesController : ApplicationController
    {
        public SpeciesController(ILogger<ApplicationController> logger) : base(logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<SpeciesDto>>> GetAllSpecies(
            [FromQuery] GetSpeciesFilteredWithPaginationRequest request,
            [FromServices] GetSpeciesFilteredWithPaginationHandler handler,
            CancellationToken cancellationToken)
        {
            var query = request.ToQuery();
            
            var result = await handler.HandleAsync(query, cancellationToken);
            return result.ToResponse();
        }
        
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSpecies(
            [FromServices] CreateSpeciesHandler handler,
            [FromBody] CreateSpeciesRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.ExecuteAsync(request.ToCommand(), cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteSpecies(
            [FromServices] DeleteSpeciesHandler handler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var command = new DeleteSpeciesCommand(id);

            var result = await handler.ExecuteAsync(command, cancellationToken);

            return result.ToResponse();
        }
        
        [HttpDelete("{speciesId:guid}/breed/{breedId:guid}")]
        public async Task<ActionResult<Guid>> DeleteSpecies(
            Guid speciesId,
            Guid breedId,
            [FromServices] DeleteBreedHandler handler,
            CancellationToken cancellationToken)
        {
            var query = new DeleteBreedCommand(speciesId, breedId);
            
            var result = await handler.ExecuteAsync(query, cancellationToken);
            return result.ToResponse();
        }

        [HttpPost("{speciesId:guid}/breed")]
        public async Task<ActionResult<Guid>> AddBreed(
            [FromServices] CreateBreedHandler handler,
            [FromRoute] Guid speciesId,
            [FromBody] string breedName,
            CancellationToken cancellationToken = default)
        {
            var request = new CreateBreedRequest(speciesId, breedName);

            var result = await handler.ExecuteAsync(request.ToCommand(), cancellationToken);

            return result.ToResponse();
        }
    }
}
