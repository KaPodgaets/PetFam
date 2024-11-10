using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;
using PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public class UpdateRequisitesHandler : ICommandHandler<Guid, UpdateRequisitesCommand>
    {
        private readonly IVolunteerRepository _repository;
        private readonly IValidator<UpdateRequisitesCommand> _validator;
        private readonly ILogger _logger;

        public UpdateRequisitesHandler(
            IVolunteerRepository repository,
            ILogger<UpdateMainInfoHandler> logger,
            IValidator<UpdateRequisitesCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }

        public async Task<Result<Guid>> ExecuteAsync(
            UpdateRequisitesCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            List<Requisite> requisites = VolunteerDtoMappers.MapRequisiteModel(command.Requisites);

            var requisiteDetails = RequisitesDetails.Create(requisites).Value;

            var volunteerId = VolunteerId.Create(command.Id);
            var existingVolunteerByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVolunteerByIdResult.IsFailure)
            {
                return Errors.General.NotFound(command.Id).ToErrorList();
            }

            var volunteer = existingVolunteerByIdResult.Value;
            volunteer.UpdateRequisite(requisiteDetails);

            var updateResult = await _repository.Update(volunteer, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Errors.General.Failure().ToErrorList();
            }

            _logger.LogInformation(
                "Requisites for volunteer with {id} were updated",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
