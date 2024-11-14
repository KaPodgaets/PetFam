using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Application.Extensions;
using PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Shared;
using PetFam.Shared.ValueObjects.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public class UpdateSocialMediaHandler : ICommandHandler<Guid, UpdateSocialMediaCommand>
    {
        private readonly IVolunteerRepository _repository;
        private readonly IValidator<UpdateSocialMediaCommand> _validator;
        private readonly ILogger _logger;

        public UpdateSocialMediaHandler(
            IVolunteerRepository repository,
            ILogger<UpdateMainInfoHandler> logger,
            IValidator<UpdateSocialMediaCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> ExecuteAsync(
            UpdateSocialMediaCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            List<SocialMediaLink> links = VolunteerDtoMappers.MapSocialMediaLinkModel(command.SocialMediaLinks);

            var socialMediaDetails = SocialMediaDetails.Create(links).Value;

            var volunteerId = VolunteerId.Create(command.Id);
            var existingVolunteerByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVolunteerByIdResult.IsFailure)
            {
                return Errors.General.NotFound(command.Id).ToErrorList();
            }

            var volunteer = existingVolunteerByIdResult.Value;
            volunteer.UpdateSocialMedia(socialMediaDetails);

            var updateResult = await _repository.Update(volunteer, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Errors.General.Failure().ToErrorList();
            }

            _logger.LogInformation(
                "Social Media for volunteer with {id} was updated",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
