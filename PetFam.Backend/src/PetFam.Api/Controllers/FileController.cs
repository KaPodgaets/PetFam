using Microsoft.AspNetCore.Mvc;
using Minio;
using PetFam.Api.Extensions;
using PetFam.Application.FileManagement.Delete;
using PetFam.Application.FileManagement.GetLink;
using PetFam.Application.FileManagement.Upload;
using PetFam.Application.FileProvider;
using PetFam.Infrastructure.Options;

namespace PetFam.Api.Controllers
{
    public class FileController : ApplicationController
    {
        public FileController(ILogger<VolunteerController> logger)
            : base(logger)
        {
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateFile(
            IFormFile file,
            [FromServices] IUploadFileHandler uploadFileHandler,
            CancellationToken cancellationToken = default)
        {

            await using var stream = file.OpenReadStream();

            var fileName = Guid.NewGuid().ToString();
            var fileData = new FileData(
                stream,
                new FileMetedata(MinioOptions.PHOTO_BUCKET, fileName));

            var request = new UploadFileRequest(fileData);

            var result = await uploadFileHandler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<string>> GetDownloadLink(
            [FromServices] GetFileLinkHandler getFileLinkHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var fileMetadata = new FileMetedata(MinioOptions.PHOTO_BUCKET, id.ToString());

            var request = new GetFileLinkRequest(fileMetadata);

            var result = await getFileLinkHandler.Handle(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteFile(
            [FromServices] DeleteFileHandler deleteFileHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var fileMetadata = new FileMetedata(MinioOptions.PHOTO_BUCKET, id.ToString());

            var request = new DeleteFileRequest(fileMetadata);

            var result = await deleteFileHandler.Handle(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
