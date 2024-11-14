using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;
using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.Delete
{
    public class DeleteHandler : ICommandHandler<Guid, DeleteCommand>
    {
        private readonly IVolunteerRepository _repository;
        private readonly ILogger _logger;

        public DeleteHandler(
            IVolunteerRepository repository,
            ILogger<DeleteHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid>> ExecuteAsync(
            DeleteCommand request,
            CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.Create(request.Id);
            var existingVolunteerByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVolunteerByIdResult.IsFailure)
            {
                return Errors.General.NotFound(request.Id).ToErrorList();
            }

            var volunteer = existingVolunteerByIdResult.Value;
            volunteer.Delete();

            var updateResult = await _repository.Update(volunteer, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Errors.General.Failure().ToErrorList();
            }

            _logger.LogInformation(
                "Volunteer with {id} was deleted",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
