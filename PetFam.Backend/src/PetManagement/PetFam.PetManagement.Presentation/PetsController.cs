using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PetFam.Framework;
using PetFam.Framework.Authorization;
using PetFam.Shared.Dtos;
using PetFam.Shared.Models;
using PetFam.PetManagement.Application.VolunteerManagement.Queries.GetPetById;
using PetFam.PetManagement.Application.VolunteerManagement.Queries.GetPets;
using PetFam.PetManagement.Presentation.Requests;

namespace PetFam.PetManagement.Presentation
{
    [Authorize]
    public class PetsController : ApplicationController
    {
        public PetsController(ILogger<ApplicationController> logger) : base(logger)
        {
        }
        
        [Permission(Permissions.Pets.Read)]
        [HttpGet("{petId:guid}")]
        public async Task<ActionResult<PetDto>> GetPetById(
            [FromRoute] Guid petId,
            [FromServices] GetPetByIdHandler handler,
            CancellationToken cancellation)
        {
            var command = new GetPetByIdQuery(petId);
            
            var result = await handler.HandleAsync(command, cancellation);
            
            return result.ToResponse();
        }
        
        [Permission(Permissions.Pets.Read)]
        [HttpGet]
        public async Task<ActionResult<PagedList<PetDto>>> GetFilteredPetsWithPagination(
            [FromQuery] GetFilteredPetsWithPaginationRequest request,
            [FromServices] GetFilteredPetsWithPaginationHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            var result = await handler.HandleAsync(query, cancellationToken);

            return result.ToResponse();
        }
    }
}
