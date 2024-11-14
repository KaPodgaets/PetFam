using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.ChangePetMainPhoto;

public class ChangePetMainPhotoHandler
    : ICommandHandler<string, ChangePetMainPhotoCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<ChangePetMainPhotoHandler> _logger;
    private readonly IValidator<ChangePetMainPhotoCommand> _validator;

    public ChangePetMainPhotoHandler(
        IVolunteerRepository repository,
        ILogger<ChangePetMainPhotoHandler> logger,
        IValidator<ChangePetMainPhotoCommand> validator)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<string>> ExecuteAsync(
        ChangePetMainPhotoCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var getVolunteerResult = await _repository.GetById(volunteerId, cancellationToken);
        if (getVolunteerResult.IsFailure)
            return getVolunteerResult.Errors;

        var volunteer = getVolunteerResult.Value;
        var petPhoto = PetPhoto.Create(command.Path, true).Value;
        var changeMainPhotoResult = volunteer.ChangeMainPhoto(PetId.Create(command.PetId), petPhoto);
        if (changeMainPhotoResult.IsFailure)
            return changeMainPhotoResult.Errors;

        var saveResult = await _repository.Update(volunteer, cancellationToken);
        if (saveResult.IsFailure)
            return saveResult.Errors;

        return command.Path;
    }
}