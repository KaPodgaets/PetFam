using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Commands.Update;

public class UpdateHandler
    : ICommandHandler<Guid, UpdateCommand>
{
    private readonly ILogger<ICommandHandler<Guid, UpdateCommand>> _logger;
    private readonly IApplicationsRepository _repository;
    private readonly IValidator<UpdateCommand> _validator;

    public UpdateHandler(
        ILogger<ICommandHandler<Guid, UpdateCommand>> logger,
        IApplicationsRepository repository,
        IValidator<UpdateCommand> validator)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
    }

    public async Task<Result<Guid>> ExecuteAsync(
        UpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        var getApplicationResult = await _repository.GetById(
            VolunteeringApplicationId.Create(command.Id),
            cancellationToken);

        if (getApplicationResult.IsFailure)
            return Errors.General.NotFound("application not found").ToErrorList();

        var changeStatusResult = getApplicationResult.Value.Update(command.VolunteerInfo);
        if (changeStatusResult.IsFailure)
            return changeStatusResult.Errors;

        var result = await _repository.Update(getApplicationResult.Value, cancellationToken);
        if (result.IsFailure)
            return result;

        _logger.LogInformation("Volunteering application updated");

        return command.Id;
    }
}