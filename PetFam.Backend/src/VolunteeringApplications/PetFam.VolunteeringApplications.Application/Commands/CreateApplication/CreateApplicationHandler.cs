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

namespace PetFam.VolunteeringApplications.Application.Commands.CreateApplication;

public class CreateApplicationHandler
    : ICommandHandler<Guid, CreateCommand>
{
    private readonly ILogger<CreateApplicationHandler> _logger;
    private readonly IApplicationsRepository _repository;
    private readonly IValidator<CreateCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateApplicationHandler(
        ILogger<CreateApplicationHandler> logger,
        IApplicationsRepository repository,
        IValidator<CreateCommand> validator,
        [FromKeyedServices(Modules.Applications)]
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> ExecuteAsync(
        CreateCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        // TODO: check that user exists

        var application = VolunteeringApplication.CreateNewApplication(
            command.UserId,
            command.VolunteerInfo).Value;

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        var result = await _repository.Add(application, cancellationToken);
        if (result.IsFailure)
            return result;
        try
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            transaction.Commit();
        }
        catch (Exception e)
        {
            _logger.LogError("Can't create new volunteering application. Error - {error}", e.Message);
            transaction.Rollback();
            return Errors.General.Failure().ToErrorList();
        }

        return result.Value;
    }
}