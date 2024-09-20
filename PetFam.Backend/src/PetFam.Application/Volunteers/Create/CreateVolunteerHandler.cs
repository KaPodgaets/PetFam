using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.Create
{
    public class CreateVolunteerHandler : ICreateVolunteerHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly ILogger _logger;

        public CreateVolunteerHandler(
            IVolunteerRepository repository,
            ILogger<CreateVolunteerHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Result<Guid>> Execute(CreateVolunteerRequest request,
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

            List<SocialMediaLink> socialMediaLinks = MapSocialMediaLinkModel(request);

            var createSocialMediaDetailsResult = SocialMediaDetails.Create(socialMediaLinks);

            if (createSocialMediaDetailsResult.IsFailure)
            {
                return Result<Guid>.Failure(createSocialMediaDetailsResult.Error);
            }

            List<Requisite> requisites = MapRequisiteModel(request);

            var createRequisiteDetailsResult = RequisitesDetails.Create(requisites);

            if (createRequisiteDetailsResult.IsFailure)
            {
                return Result<Guid>.Failure(createRequisiteDetailsResult.Error);
            }

            var createEmailResult = Email.Create(request.Email);

            if (createEmailResult.IsFailure)
            {
                return Result<Guid>.Failure(createEmailResult.Error);
            }

            var existingVoluntreeByEmailResult = await _repository.GetByEmail(createEmailResult.Value);

            if (existingVoluntreeByEmailResult.IsSuccess)
            {
                return Result<Guid>.Failure(
                    Errors.Volunteer.AlreadyExist(
                        createEmailResult.Value.Value));
            }

            var volunteerCreationResult = Volunteer.Create(
                VolunteerId.NewId(),
                fullNameCreationResult.Value,
                createEmailResult.Value,
                createSocialMediaDetailsResult.Value,
                createRequisiteDetailsResult.Value);

            if (volunteerCreationResult.IsFailure)
                return Result<Guid>.Failure(volunteerCreationResult.Error);

            var creationResult = await _repository.Add(volunteerCreationResult.Value, cancellationToken);

            _logger.LogInformation(
                "Created volunteer with {email} with id {id}",
                createEmailResult.Value,
                creationResult.Value);

            return creationResult;
        }

        private static List<Requisite> MapRequisiteModel(CreateVolunteerRequest request)
        {
            return request.Requisites?
                            .Select(requisiteDto => Requisite.Create(requisiteDto.Name,
                                requisiteDto.AccountNumber,
                                requisiteDto.PaymentInstruction))
                            .Where(createResult => createResult.IsSuccess)
                            .Select(createResult => createResult.Value)
                            .ToList() ?? [];
        }

        private static List<SocialMediaLink> MapSocialMediaLinkModel(CreateVolunteerRequest request)
        {
            return request.SocialMediaLinks?
                            .Select(linkDto => SocialMediaLink.Create(linkDto.name, linkDto.link))
                            .Where(createResult => createResult.IsSuccess)
                            .Select(createResult => createResult.Value)
                            .ToList() ?? [];
        }
    }
}
