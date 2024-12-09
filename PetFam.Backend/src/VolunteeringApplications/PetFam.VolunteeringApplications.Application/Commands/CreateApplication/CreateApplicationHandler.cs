using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.CreateApplication;

public class CreateApplicationHandler
    :ICommandHandler<Guid, CreateCommand>
{
    private readonly ILogger<CreateApplicationHandler> _logger;
    
    public CreateApplicationHandler(
        ILogger<CreateApplicationHandler> logger)
    {
        _logger = logger;
    }
    public Task<Result<Guid>> ExecuteAsync(
        CreateCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}