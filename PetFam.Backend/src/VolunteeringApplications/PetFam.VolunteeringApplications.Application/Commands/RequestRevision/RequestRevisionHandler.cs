using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Commands.Shared;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Commands.RequestRevision;

public class RequestRevisionHandler:ICommandHandler<Guid, RejectApplicationCommand>
{
    private readonly ILogger<RequestRevisionHandler> _logger;
    private readonly IApplicationsRepository _repository;
    private readonly IValidator<RejectApplicationCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public RequestRevisionHandler(
        ILogger<RequestRevisionHandler> logger,
        IApplicationsRepository repository,
        IValidator<RejectApplicationCommand> validator,
        [FromKeyedServices(Modules.Applications)] IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
        _unitOfWork = unitOfWork;
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
        
        var changeStatusResult = getApplicationResult.Value.RequestRevision(command.Comment);
        if (changeStatusResult.IsFailure) 
            return changeStatusResult.Errors;
        
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        try
        {
            var result = _repository.Update(getApplicationResult.Value, cancellationToken);
            if (result.IsFailure)
                return result;
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError("Can't change application status. Error - {error}", e.Message);
            transaction.Rollback();
            return Errors.General.Failure().ToErrorList();
        }
        
        return command.Id;
    }
}