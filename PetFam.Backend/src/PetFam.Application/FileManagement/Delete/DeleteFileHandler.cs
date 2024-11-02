using Microsoft.Extensions.Logging;
using PetFam.Application.FileProvider;
using PetFam.Application.VolunteerManagement.Create;
using PetFam.Domain.Shared;

namespace PetFam.Application.FileManagement.Delete
{
    public class DeleteFileHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public DeleteFileHandler(
            IFileProvider fileProvider,
            ILogger<CreateVolunteerHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }
        public async Task<Result> Execute(DeleteFileCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await _fileProvider.DeleteFile(request.FileMetadata, cancellationToken);

            return result;
        }
    }
}
