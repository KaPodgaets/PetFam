using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateName
{
    public class VolunteerUpdateMainInfoHandler : IVolunteerUpdateMainInfoHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly ILogger _logger;

        public VolunteerUpdateMainInfoHandler(
            IVolunteerRepository repository,
            ILogger<VolunteerUpdateMainInfoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(
            UpdateMainInfoRequest request,
            CancellationToken cancellationToken = default)
        {
            var fullName = FullName.Create(
                request.Dto.FullNameDto.FirstName,
                request.Dto.FullNameDto.LastName,
                request.Dto.FullNameDto.Patronimycs)
                .Value;

            var volunteerId = VolunteerId.Create(request.Id);
            var existingVoluntreeByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVoluntreeByIdResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.NotFound(
                        request.Id));
            }

            var volunteer = existingVoluntreeByIdResult.Value;
            volunteer.UpdateMainInfo(fullName);

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
