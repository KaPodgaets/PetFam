using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PetFam.Api.Response;

namespace PetFam.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApplicationController : ControllerBase
    {
        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            var envelope = Envelope.Ok(value);

            return base.Ok(envelope);
        }
    }
}
