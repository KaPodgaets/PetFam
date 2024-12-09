using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Commands.CreateApplication;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;

public class AssignAdminHandler
    : ICommandHandler<Guid, AssignAdminCommand>
{
    private readonly ILogger<ICommandHandler<Guid, AssignAdminCommand>> _logger;
    private readonly IApplicationsRepository _repository;
    private readonly IValidator<AssignAdminCommand> _validator;

    public AssignAdminHandler(
        ILogger<ICommandHandler<Guid, AssignAdminCommand>> logger,
        IApplicationsRepository repository,
        IValidator<AssignAdminCommand> validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid>> ExecuteAsync(
        AssignAdminCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();
        
        // TODO: check that admin exists
        
        var getApplicationResult = await _repository.GetById(
            VolunteeringApplicationId.Create(command.Id),
            cancellationToken);

        if (getApplicationResult.IsFailure)
            return Errors.General.NotFound("application not found").ToErrorList();

        getApplicationResult.Value.AssignAdmin(command.AdminId);
        var result = await _repository.Update(getApplicationResult.Value, cancellationToken);
        if (result.IsFailure)
            return result;

        return command.Id;
    }
}