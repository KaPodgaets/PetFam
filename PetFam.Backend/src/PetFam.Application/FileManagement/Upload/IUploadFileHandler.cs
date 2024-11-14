using PetFam.Shared.SharedKernel;

namespace PetFam.Application.FileManagement.Upload
{
    public interface IUploadFileHandler
    {
        Task<Result> Execute(UploadFileCommand request,
            CancellationToken cancellationToken = default);
    }
}