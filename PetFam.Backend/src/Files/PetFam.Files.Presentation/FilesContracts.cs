using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Files.Application.FileManagement.Upload;
using PetFam.Files.Contracts;
using PetFam.Shared.Dtos;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Files.Presentation;

public class FilesContracts : IFilesContracts
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<FilesContracts> _logger;

    public FilesContracts(
        IServiceProvider serviceProvider,
        ILogger<FilesContracts> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<Result> UploadFilesAsync(Content content, CancellationToken cancellationToken = default)
    {
        var command = new UploadFileCommand(content);
        
        await using var scope = _serviceProvider.CreateAsyncScope();
        
        var uploadHandler = scope.ServiceProvider.GetRequiredService<UploadFileHandler>();
        
        var result = await uploadHandler.ExecuteAsync(command, cancellationToken);
        if(result.IsFailure)
            return result;

        return Result.Success();
    }
}