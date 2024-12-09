using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;

public class AssignAdminHandler
    :ICommandHandler<Guid, AssignAdminCommand>
{
    private readonly ILogger<ICommandHandler<Guid, AssignAdminCommand>> _logger;
    
    public AssignAdminHandler(
        ILogger<ICommandHandler<Guid, AssignAdminCommand>> logger)
    {
        _logger = logger;
    }
    public Task<Result<Guid>> ExecuteAsync(
        AssignAdminCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}