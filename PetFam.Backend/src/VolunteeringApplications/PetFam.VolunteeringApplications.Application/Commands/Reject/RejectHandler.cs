using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.Reject;

public class RejectHandler:ICommandHandler<Guid, ChangeApplicationStatusCommand>
{
    private readonly ILogger<RejectHandler> _logger;
    
    public RejectHandler(
        ILogger<RejectHandler> logger)
    {
        _logger = logger;
    }
    public Task<Result<Guid>> ExecuteAsync(
        ChangeApplicationStatusCommand command,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}