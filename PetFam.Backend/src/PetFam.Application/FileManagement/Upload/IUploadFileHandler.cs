using PetFam.Domain.Shared;

namespace PetFam.Application.FileManagement.Upload
{
    public interface IUploadFileHandler
    {
        Task<Result> Execute(UploadFileCommand request,
            CancellationToken cancellationToken = default);
    }
}