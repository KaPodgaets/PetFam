using Microsoft.Extensions.Logging;
using PetFam.Application.Volunteers.UpdateMainInfo;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateSocialMedia
{
    public class UpdateSocialMediaHandler : IUpdateSocialMediaHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly ILogger _logger;

        public UpdateSocialMediaHandler(
            IVolunteerRepository repository,
            ILogger<UpdateMainInfoHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result<Guid>> Handle(
            UpdateSocialMediaRequest request,
            CancellationToken cancellationToken = default)
        {
            List<SocialMediaLink> links = VolunteerDtoMappers.MapSocialMediaLinkModel(request.Dto.SocialMediaLinks);

            var socialMediaDetails = SocialMediaDetails.Create(links).Value;

            var volunteerId = VolunteerId.Create(request.Id);
            var existingVoluntreeByIdResult = await _repository.GetById(volunteerId, cancellationToken);

            if (existingVoluntreeByIdResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.NotFound(
                        request.Id));
            }

            var volunteer = existingVoluntreeByIdResult.Value;
            volunteer.UpdateSocialMedia(socialMediaDetails);

            var updateResult = await _repository.Update(volunteer, cancellationToken);
            if (updateResult.IsFailure)
            {
                return Result<Guid>.Failure(
                    Errors.General.Failure());
            }

            _logger.LogInformation(
                "Social Media for volunteer with {id} was updated",
                volunteer.Id.Value);

            return updateResult;
        }
    }
}
