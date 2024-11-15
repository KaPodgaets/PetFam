using Microsoft.Extensions.Logging;
using PetFam.Files.Application.FileProvider;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Files.Application.FileManagement.GetLink
{
    public class GetFileLinkHandler:ICommandHandler<string,GetFileLinkCommand>
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public GetFileLinkHandler(
            IFileProvider fileProvider,
            ILogger<GetFileLinkHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }
        public async Task<Result<string>> ExecuteAsync(GetFileLinkCommand request,
            CancellationToken cancellationToken = default)
        {

            var result = await _fileProvider.GetDownloadLink(request.FileMetadata, cancellationToken);

            return result;
        }
    }
}
