using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.Update;

public class UpdateCommandHandler
    :ICommandHandler<Guid, UpdateCommand>
{
    private readonly ILogger<ICommandHandler<Guid, UpdateCommand>> _logger;
    
    public UpdateCommandHandler(
        ILogger<ICommandHandler<Guid, UpdateCommand>> logger)
    {
        _logger = logger;
    }
    public Task<Result<Guid>> ExecuteAsync(
        UpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}