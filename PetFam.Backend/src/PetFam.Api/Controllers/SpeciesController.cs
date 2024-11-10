using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Requests.Species;
using PetFam.Application.SpeciesManagement.Create;
using PetFam.Application.SpeciesManagement.CreateBreed;
using PetFam.Application.SpeciesManagement.Delete;

namespace PetFam.Api.Controllers
{
    public class SpeciesController : ApplicationController
    {
        public SpeciesController(ILogger<ApplicationController> logger) : base(logger)
        {
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
