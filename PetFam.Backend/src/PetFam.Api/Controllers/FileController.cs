using Microsoft.AspNetCore.Mvc;

namespace PetFam.Api.Controllers
{
    public class FileController : ApplicationController
    {
        public FileController(ILogger<VolunteerController> logger)
            : base(logger)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateFile(IFormFile file)
        {
            await using var stream = file.OpenReadStream();
            return Ok();
        }
    }
}
