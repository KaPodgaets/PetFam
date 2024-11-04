using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.Delete
{
    public class DeleteHandler : IDeleteHandler
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

        public async Task<Result<Guid>> Execute(
            DeleteCommand request,
            CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.Create(request.Id);
            var existingVoluntreeByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVoluntreeByIdResult.IsFailure)
            {
                return Errors.General.NotFound(request.Id).ToErrorList();
            }

            var volunteer = existingVoluntreeByIdResult.Value;
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
