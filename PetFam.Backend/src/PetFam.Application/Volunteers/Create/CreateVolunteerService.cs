using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.Create
{
    public class CreateVolunteerService : ICreateVolunteerService
    {
        private readonly IVolunteerRepository _repository;

        public CreateVolunteerService(IVolunteerRepository repository)
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


            var socialMediaLinks = new List<SocialMediaLink>();
            if (request.SocialMediaLinks != null && request.SocialMediaLinks.Count != 0)
            {
                foreach (var linkDto in request.SocialMediaLinks)
                {
                    var createSocialMediaLinkResult = SocialMediaLink.Create(linkDto.name, linkDto.link);

                    if (createSocialMediaLinkResult.IsSuccess)
                    {
                        socialMediaLinks.Add(createSocialMediaLinkResult.Value);
                    }
                }
            }

            var createSocialMediaDetailsResult = SocialMediaDetails.Create(socialMediaLinks);

            if (createSocialMediaDetailsResult.IsFailure)
            {
                return Result<Guid>.Failure(createSocialMediaDetailsResult.ErrorMessage);
            }

            var requisites = new List<Requisite>();
            if (request.Requisites != null && request.Requisites.Count != 0)
            {
                foreach (var requisiteDto in request.Requisites)
                {
                    var createRequisiteModelResult = Requisite.Create(
                        requisiteDto.Name,
                        requisiteDto.AccountNumber,
                        requisiteDto.PaymentInstruction);

                    if (createRequisiteModelResult.IsSuccess)
                    {
                        requisites.Add(createRequisiteModelResult.Value);
                    }
                }
            }

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
    }
}
