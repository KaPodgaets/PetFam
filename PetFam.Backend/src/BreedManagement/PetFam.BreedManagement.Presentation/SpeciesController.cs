using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.BreedManagement.Application.SpeciesManagement.Commands.Create;
using PetFam.BreedManagement.Application.SpeciesManagement.Commands.CreateBreed;
using PetFam.BreedManagement.Application.SpeciesManagement.Commands.Delete;
using PetFam.BreedManagement.Application.SpeciesManagement.Commands.DeleteBreed;
using PetFam.BreedManagement.Application.SpeciesManagement.Queries.Get;
using PetFam.BreedManagement.Presentation.Requests;
using PetFam.Framework;
using PetFam.Framework.Authorization;
using PetFam.Shared.Dtos;
using PetFam.Shared.Models;

namespace PetFam.BreedManagement.Presentation
{
    [Authorize]
    public class SpeciesController : ApplicationController
    {
        public SpeciesController(ILogger<ApplicationController> logger) : base(logger)
        {
        }
        
        [Permission(Permissions.Species.Read)]
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
        
        [Permission(Permissions.Species.Create)]
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateSpecies(
            [FromServices] CreateSpeciesHandler handler,
            [FromBody] CreateSpeciesRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await handler.ExecuteAsync(request.ToCommand(), cancellationToken);

            return result.ToResponse();
        }
        
        [Permission(Permissions.Species.Delete)]
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
        
        [Permission(Permissions.Breeds.Read)]
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
        
        [Permission(Permissions.Breeds.Create)]
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
