using PetFam.Files.Contracts;
using PetFam.Files.Contracts.Dtos;
using PetFam.Shared.Dtos;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Files.Presentation;

public class FilesContracts : IFilesContracts
{
    public Task<Result> UploadFilesAsync(Content content, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}