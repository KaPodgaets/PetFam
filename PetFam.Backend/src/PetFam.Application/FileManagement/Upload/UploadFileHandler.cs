using Microsoft.Extensions.Logging;
using PetFam.Application.VolunteerManagement.Create;
using PetFam.Domain.Shared;
using PetFam.Application.FileProvider;

namespace PetFam.Application.FileManagement.Upload
{
    public class UploadFileHandler : IUploadFileHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public UploadFileHandler(
            IFileProvider fileProvider,
            ILogger<CreateVolunteerHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }
        public async Task<Result> Handle(UploadFileRequest request,
            CancellationToken cancellationToken = default)
        {
            var result = await _fileProvider.UploadFiles(request.Content, cancellationToken);

            return result;
        }
    }
}
