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
                new FileMetadata(MinioOptions.PHOTO_BUCKET, fileName));

            List<FileData> files = [fileData];
            var content = new Content(files, MinioOptions.PHOTO_BUCKET);

            var request = new UploadFileCommand(content);

            var result = await uploadFileHandler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<string>> GetDownloadLink(
            [FromServices] GetFileLinkHandler getFileLinkHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var fileMetadata = new FileMetadata(MinioOptions.PHOTO_BUCKET, id.ToString());

            var request = new GetFileLinkCommand(fileMetadata);

            var result = await getFileLinkHandler.Execute(request, cancellationToken);

            return result.ToResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteFile(
            [FromServices] DeleteFileHandler deleteFileHandler,
            [FromRoute] Guid id,
            CancellationToken cancellationToken = default)
        {
            var fileMetadata = new FileMetadata(MinioOptions.PHOTO_BUCKET, id.ToString());

            var request = new DeleteFileCommand(fileMetadata);

            var result = await deleteFileHandler.Execute(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
