using PetFam.Files.Contracts.Dtos;
using PetFam.Shared.Dtos;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Files.Contracts;

public interface IFilesContracts
{
    Task<Result> UploadFilesAsync(Content content, CancellationToken cancellationToken = default);
}