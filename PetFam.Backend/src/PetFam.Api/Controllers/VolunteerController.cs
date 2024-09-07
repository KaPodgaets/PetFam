using Microsoft.AspNetCore.Mvc;
using PetFam.Application.Volunteers.Create;

namespace PetFam.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] ICreateVolunteerService service,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await service.Execute(request, cancellationToken);

            if (result.IsFailure)
            {
                BadRequest(result.ErrorMessage);
            }

            return Ok(result.Value);
        }
    }
}
