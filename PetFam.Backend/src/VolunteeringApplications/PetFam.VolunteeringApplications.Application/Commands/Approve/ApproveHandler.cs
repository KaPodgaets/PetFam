using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.Approve;

public class ApproveHandler:ICommandHandler<Guid, ChangeApplicationStatusCommand>
{
    private readonly ILogger<ApproveHandler> _logger;
    
    public ApproveHandler(
        ILogger<ApproveHandler> logger)
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