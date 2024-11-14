using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Domain.Volunteer;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.Shared;
using PetFam.Shared.ValueObjects.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.Create
{
    public class CreateVolunteerHandler : ICommandHandler<Guid, CreateVolunteerCommand>
    {
        private readonly IVolunteerRepository _repository;
        private readonly IValidator<CreateVolunteerCommand> _validator;
        private readonly ILogger _logger;

        public CreateVolunteerHandler(
            IVolunteerRepository repository,
            ILogger<CreateVolunteerHandler> logger,
            IValidator<CreateVolunteerCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> ExecuteAsync(CreateVolunteerCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);
            if (validationResult.IsValid is false)
                return validationResult.ToErrorList();

            var fullName = FullName.Create(
                    command.FullNameDto.FirstName,
                    command.FullNameDto.LastName,
                    command.FullNameDto.Patronimycs)
                .Value;

            List<SocialMediaLink> socialMediaLinks = VolunteerDtoMappers.MapSocialMediaLinkModel(command);

            var createSocialMediaDetailsResult = SocialMediaDetails.Create(socialMediaLinks);

            if (createSocialMediaDetailsResult.IsFailure)
            {
                return Result<Guid>.Failure(createSocialMediaDetailsResult.Errors);
            }

            List<Requisite> requisites = VolunteerDtoMappers.MapRequisiteModel(command);

            var createRequisiteDetailsResult = RequisitesDetails.Create(requisites);

            if (createRequisiteDetailsResult.IsFailure)
            {
                return Result<Guid>.Failure(createRequisiteDetailsResult.Errors);
            }

            var createEmailResult = Email.Create(command.Email);

            if (createEmailResult.IsFailure)
            {
                return Result<Guid>.Failure(createEmailResult.Errors);
            }

            var existingVoluntreeByEmailResult = await _repository.GetByEmail(
                createEmailResult.Value,
                cancellationToken);

            if (existingVoluntreeByEmailResult.IsSuccess)
            {
                return Errors.Volunteer.AlreadyExist(createEmailResult.Value.Value).ToErrorList();
            }

            var volunteerCreationResult = Volunteer.Create(
                VolunteerId.NewId(),
                fullName,
                createEmailResult.Value,
                createSocialMediaDetailsResult.Value,
                createRequisiteDetailsResult.Value);

            if (volunteerCreationResult.IsFailure)
                return Result<Guid>.Failure(volunteerCreationResult.Errors);

            var creationResult = await _repository.Add(volunteerCreationResult.Value, cancellationToken);

            _logger.LogInformation(
                "Created volunteer with {email} with id {id}",
                createEmailResult.Value,
                creationResult.Value);

            return creationResult;
        }
    }
}