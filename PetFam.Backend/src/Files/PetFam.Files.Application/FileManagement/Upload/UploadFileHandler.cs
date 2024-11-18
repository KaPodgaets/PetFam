using Microsoft.Extensions.Logging;
using PetFam.Files.Application.FileProvider;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Files.Application.FileManagement.Upload
{
    public class UploadFileHandler : ICommandHandler<UploadFileCommand>
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public UploadFileHandler(
            IFileProvider fileProvider,
            ILogger<UploadFileHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }
        public async Task<Result> ExecuteAsync(UploadFileCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await _fileProvider.UploadFiles(request.Content, cancellationToken);

            return result;
        }
    }
}
