using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel;
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
    private readonly IUnitOfWork _unitOfWork;

    public UpdateHandler(
        ILogger<ICommandHandler<Guid, UpdateCommand>> logger,
        IApplicationsRepository repository,
        IValidator<UpdateCommand> validator,
        [FromKeyedServices(Modules.Applications)] IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
        _unitOfWork = unitOfWork;
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

        _logger.LogInformation("Volunteering application updated");

        return command.Id;
    }
}