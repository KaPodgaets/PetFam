using Microsoft.AspNetCore.Mvc;
using Minio;
using PetFam.Api.Extensions;
using PetFam.Application.FileManagement.Upload;
using PetFam.Application.FileProvider;
using PetFam.Infrastructure.Options;

namespace PetFam.Api.Controllers
{
    public class FileController : ApplicationController
    {
        private readonly IMinioClient _minioClient;

        public FileController(ILogger<VolunteerController> logger, IMinioClient minioClient)
            : base(logger)
        {
            _minioClient = minioClient;
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateFile(
            IFormFile file,
            [FromServices] IUploadFileHandler uploadFileHandler,
            CancellationToken cancellationToken = default)
        {
            await using var stream = file.OpenReadStream();

            var fileName = Guid.NewGuid().ToString();

            var fileData = new FileData(stream, MinioOptions.PHOTO_BUCKET, fileName);
            var request = new UploadFileRequest(fileData);

            var result = await uploadFileHandler.Handle(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
