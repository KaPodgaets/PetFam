namespace PetFam.Files.Application.FileManagement.GetLink
{
    public class GetFileLinkHandler
    {
        private readonly IFileProvider _fileProvider;
        private readonly ILogger _logger;

        public GetFileLinkHandler(
            IFileProvider fileProvider,
            ILogger<CreateVolunteerHandler> logger)
        {
            _fileProvider = fileProvider;
            _logger = logger;
        }
        public async Task<Result<string>> Execute(GetFileLinkCommand request,
            CancellationToken cancellationToken = default)
        {

            var result = await _fileProvider.GetDownloadLink(request.FileMetadata, cancellationToken);

            return result;
        }
    }
}
