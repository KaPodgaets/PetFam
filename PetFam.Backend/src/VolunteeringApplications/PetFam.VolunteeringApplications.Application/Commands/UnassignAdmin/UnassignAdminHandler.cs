using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Commands.UnassignAdmin;

public class UnassignAdminHandler
    :ICommandHandler<Guid, UnassignAdminCommand>
{
    private readonly ILogger<ICommandHandler<Guid, UnassignAdminCommand>> _logger;
    private readonly IApplicationsRepository _repository;
    private readonly IValidator<UnassignAdminCommand> _validator;
    
    public UnassignAdminHandler(
        ILogger<ICommandHandler<Guid, UnassignAdminCommand>> logger,
        IApplicationsRepository repository,
        IValidator<UnassignAdminCommand> validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
    }
    public async Task<Result<Guid>> ExecuteAsync(
        UnassignAdminCommand command,
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
        
        getApplicationResult.Value.UnassignAdmin();
        var result = await _repository.Update(getApplicationResult.Value, cancellationToken);
        if (result.IsFailure)
            return result;
        
        return command.Id;
    }
}