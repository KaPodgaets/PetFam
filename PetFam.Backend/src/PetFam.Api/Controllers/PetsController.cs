
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Extensions;
using PetFam.Api.Requests.Pets;
using PetFam.Application;
using PetFam.Application.Dtos;
using PetFam.Application.VolunteerManagement.Queries.GetPets;

namespace PetFam.Api.Controllers
{
    public class PetsController : ApplicationController
    {
        public PetsController(ILogger<ApplicationController> logger) : base(logger)
        {
        }

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
