using PetFam.Volunteers.Application.FileProvider;
using PetFam.Volunteers.Application.VolunteerManagement.Commands.Create;

namespace PetFam.Volunteers.Application.FileManagement.Upload
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
        public async Task<Result> Execute(UploadFileCommand request,
            CancellationToken cancellationToken = default)
        {
            var result = await _fileProvider.UploadFiles(request.Content, cancellationToken);

            return result;
        }
    }
}
