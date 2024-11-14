using PetFam.Shared.Shared;

namespace PetFam.Application.FileManagement.Upload
{
    public interface IUploadFileHandler
    {
        Task<Result> Execute(UploadFileCommand request,
            CancellationToken cancellationToken = default);
    }
}