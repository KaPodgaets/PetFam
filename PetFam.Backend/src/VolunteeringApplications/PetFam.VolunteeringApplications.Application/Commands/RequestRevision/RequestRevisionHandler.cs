using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.RequestRevision;

public class RequestRevisionHandler:ICommandHandler<Guid, ChangeApplicationStatusCommand>
{
    private readonly ILogger<RequestRevisionHandler> _logger;
    
    public RequestRevisionHandler(
        ILogger<RequestRevisionHandler> logger)
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