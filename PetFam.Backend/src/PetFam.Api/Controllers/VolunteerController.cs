using Microsoft.AspNetCore.Mvc;
using PetFam.Api.Controllers;
using PetFam.Application.Extensions;
using PetFam.Application.Volunteers.Create;

namespace PetFam.Application.Controllers
{
    public class VolunteerController : ApplicationController
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] ICreateVolunteerHandler service,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await service.Execute(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
