using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.UnassignAdmin;

public class UnassignAdminHandler
    :ICommandHandler<Guid, UnassignAdminCommand>
{
    private readonly ILogger<ICommandHandler<Guid, UnassignAdminCommand>> _logger;
    
    public UnassignAdminHandler(
        ILogger<ICommandHandler<Guid, UnassignAdminCommand>> logger)
    {
        _logger = logger;
    }
    public Task<Result<Guid>> ExecuteAsync(
        UnassignAdminCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}