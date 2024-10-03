using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateName
{
    public class VolunteerUpdateNameHandler : IVolunteerUpdateNameHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly ILogger _logger;

        public VolunteerUpdateNameHandler(
            IVolunteerRepository repository,
            ILogger<VolunteerUpdateNameHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Execute(
            VolunteerUpdateNameRequest request,
            CancellationToken cancellationToken = default)
        {
            var fullNameCreationResult = FullName.Create(
                request.FullNameDto.FirstName,
                request.FullNameDto.LastName,
                request.FullNameDto.Patronimycs);

            if (fullNameCreationResult.IsFailure)
            {
                return Result<Guid>.Failure(fullNameCreationResult.Error);
            }

            var volunteerId = VolunteerId.Create(request.Id);
            var existingVoluntreeByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVoluntreeByIdResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.NotFound(
                        request.Id));
            }

            var volunteer = existingVoluntreeByIdResult.Value;
            volunteer.UpdateName(fullNameCreationResult.Value);

            var updateResult = await _repository.Update(volunteer);
            if (updateResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.Failure());
            }

            _logger.LogInformation(
                "Name for volunteer with {id} was updated",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
