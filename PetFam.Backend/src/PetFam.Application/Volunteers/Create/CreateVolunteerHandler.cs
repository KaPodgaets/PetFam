using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.Create
{
    public class CreateVolunteerHandler : ICreateVolunteerHandler
    {
        private readonly IVolunteerRepository _repository;

        public CreateVolunteerHandler(IVolunteerRepository repository)
        {
            _repository = repository;
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
                return Result<Guid>.Failure(fullNameCreationResult.ErrorMessage);
            }

            List<SocialMediaLink> socialMediaLinks = MapSocialMediaLinkModel(request);

            var createSocialMediaDetailsResult = SocialMediaDetails.Create(socialMediaLinks);

            if (createSocialMediaDetailsResult.IsFailure)
            {
                return Result<Guid>.Failure(createSocialMediaDetailsResult.ErrorMessage);
            }

            List<Requisite> requisites = MapRequisiteModel(request);

            var createRequisiteDetailsResult = RequisitesDetails.Create(requisites);

            if (createRequisiteDetailsResult.IsFailure)
            {
                return Result<Guid>.Failure(createRequisiteDetailsResult.ErrorMessage);
            }

            var volunteerCreationResult = Volunteer.Create(
                VolunteerId.NewId(),
                fullNameCreationResult.Value,
                request.Email,
                createSocialMediaDetailsResult.Value,
                createRequisiteDetailsResult.Value);

            if (volunteerCreationResult.IsFailure)
                return Result<Guid>.Failure(volunteerCreationResult.ErrorMessage);

            var creationResult = await _repository.Add(volunteerCreationResult.Value, cancellationToken);

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
