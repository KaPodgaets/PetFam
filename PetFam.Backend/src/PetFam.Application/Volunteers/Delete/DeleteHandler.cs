using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.Delete
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

        public async Task<Result<Guid>> Handle(
            DeleteRequest request,
            CancellationToken cancellationToken = default)
        {
            var volunteerId = VolunteerId.Create(request.Id);
            var existingVoluntreeByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVoluntreeByIdResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.NotFound(
                        request.Id));
            }

            var volunteer = existingVoluntreeByIdResult.Value;

            var updateResult = await _repository.Delete(volunteer, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.Failure());
            }

            _logger.LogInformation(
                "Volunteer with {id} was deleted",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
