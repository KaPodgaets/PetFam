using Microsoft.Extensions.Logging;
using PetFam.Files.Application.FileProvider;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Files.Application.FileManagement.Delete
{
    public class DeleteFileHandler:ICommandHandler<DeleteFileCommand>
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public DeleteFileHandler(
            IFileProvider fileProvider,
            ILogger<DeleteFileHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }
        public async Task<Result> ExecuteAsync(DeleteFileCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await _fileProvider.DeleteFile(request.FileMetadata, cancellationToken);

            return result;
        }
    }
}
