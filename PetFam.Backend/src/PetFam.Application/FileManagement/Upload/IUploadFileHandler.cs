using PetFam.Domain.Shared;

namespace PetFam.Application.FileManagement.Upload
{
    public interface IUploadFileHandler
    {
        Task<Result> Handle(UploadFileRequest request,
            CancellationToken cancellationToken = default);
    }
}