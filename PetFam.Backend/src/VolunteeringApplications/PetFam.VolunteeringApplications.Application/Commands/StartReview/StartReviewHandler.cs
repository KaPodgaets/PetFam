using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Application.Commands.StartReview;

public class StartReviewHandler:ICommandHandler<Guid, ChangeApplicationStatusCommand>
{
    private readonly ILogger<StartReviewHandler> _logger;
    
    public StartReviewHandler(
        ILogger<StartReviewHandler> logger)
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