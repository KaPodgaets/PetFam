
using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Requests.Pets;
using PetFam.Application.VolunteerManagement.Queries.GetPets;

namespace PetFam.Api.Controllers
{
    public class PetsController : ApplicationController
    {
        public PetsController(ILogger<ApplicationController> logger) : base(logger)
        {
        }

        [HttpGet("pets-filtered")]
        public async Task<ActionResult> GetFilteredPetsWithPagination(
            [FromQuery] GetFilteredPetsWithPaginationRequest request,
            [FromServices] GetFilteredPetsWithPaginationHandler handler,
            CancellationToken cancellationToken = default)
        {
            var query = request.ToQuery();

            var pagedList = await handler.HandleAsync(query, cancellationToken);

            return Ok(pagedList);
        }
    }
}
