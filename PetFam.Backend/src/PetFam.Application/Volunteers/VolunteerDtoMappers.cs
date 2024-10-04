using PetFam.Application.Volunteers.Create;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers
{
    public static class VolunteerDtoMappers
    {

        public static List<Requisite> MapRequisiteModel(CreateVolunteerRequest request)
        {
            return request.Requisites?
                            .Select(requisiteDto => Requisite.Create(requisiteDto.Name,
                                requisiteDto.AccountNumber,
                                requisiteDto.PaymentInstruction))
                            .Where(createResult => createResult.IsSuccess)
                            .Select(createResult => createResult.Value)
                            .ToList() ?? [];
        }

        public static List<SocialMediaLink> MapSocialMediaLinkModel(CreateVolunteerRequest request)
        {
            return request.SocialMediaLinks?
                            .Select(linkDto => SocialMediaLink.Create(linkDto.name, linkDto.link))
                            .Where(createResult => createResult.IsSuccess)
                            .Select(createResult => createResult.Value)
                            .ToList() ?? [];
        }
    }
}