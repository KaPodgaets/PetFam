﻿using Microsoft.AspNetCore.Mvc;
using PetFam.Framework;
using PetFam.Shared.SharedKernel;
using PetFam.Volunteers.Application.FileManagement.Delete;
using PetFam.Volunteers.Application.FileManagement.GetLink;
using PetFam.Volunteers.Application.FileManagement.Upload;
using PetFam.Volunteers.Application.FileProvider;

namespace PetFam.Api.Controllers
{
    public class FileController : ApplicationController
    {
        public FileController(ILogger<FileController> logger)
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
                new FileMetadata(Constants.FileManagementOptions.PHOTO_BUCKET, fileName));

            List<FileData> files = [fileData];
            var content = new Content(files, Constants.FileManagementOptions.PHOTO_BUCKET);

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
            var fileMetadata = new FileMetadata(Constants.FileManagementOptions.PHOTO_BUCKET, id.ToString());

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
            var fileMetadata = new FileMetadata(Constants.FileManagementOptions.PHOTO_BUCKET, id.ToString());

            var request = new DeleteFileCommand(fileMetadata);

            var result = await deleteFileHandler.Execute(request, cancellationToken);

            return result.ToResponse();
        }
    }
}
