using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Commands.Shared;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Commands.Reject;

public class RejectHandler:ICommandHandler<Guid, RejectApplicationCommand>
{
    private readonly ILogger<RejectHandler> _logger;
    private readonly IApplicationsRepository _repository;
    private readonly IValidator<RejectApplicationCommand> _validator;
    
    public RejectHandler(
        ILogger<RejectHandler> logger,
        IApplicationsRepository repository,
        IValidator<RejectApplicationCommand> validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
    }
    public async Task<Result<Guid>> ExecuteAsync(
        RejectApplicationCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();
        
        var getApplicationResult = await _repository.GetById(
            VolunteeringApplicationId.Create(command.Id),
            cancellationToken);
        
        if(getApplicationResult.IsFailure)
            return Errors.General.NotFound("application not found").ToErrorList();
        
        var changeStatusResult = getApplicationResult.Value.FinalReject(command.Comment);
        if (changeStatusResult.IsFailure) 
            return changeStatusResult.Errors;
        
        var result = await _repository.Update(getApplicationResult.Value, cancellationToken);
        if (result.IsFailure)
            return result;
        
        return command.Id;
    }
}