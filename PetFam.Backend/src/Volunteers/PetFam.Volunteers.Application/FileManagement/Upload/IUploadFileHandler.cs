namespace PetFam.Volunteers.Application.FileManagement.Upload
{
    public interface IUploadFileHandler
    {
        Task<Result> Execute(UploadFileCommand request,
            CancellationToken cancellationToken = default);
    }
}