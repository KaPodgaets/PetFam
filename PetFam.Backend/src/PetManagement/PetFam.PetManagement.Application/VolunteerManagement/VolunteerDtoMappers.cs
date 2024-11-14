using PetFam.PetManagement.Application.VolunteerManagement.Commands.Create;
using PetFam.Shared.Dtos.ValueObjects;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.PetManagement.Application.VolunteerManagement
{
    public static class VolunteerDtoMappers
    {

        public static List<Requisite> MapRequisiteModel(CreateVolunteerCommand request)
        {
            return request.Requisites?
                            .Select(requisiteDto => Requisite.Create(requisiteDto.Name,
                                requisiteDto.AccountNumber,
                                requisiteDto.PaymentInstruction))
                            .Where(createResult => createResult.IsSuccess)
                            .Select(createResult => createResult.Value)
                            .ToList() ?? [];
        }

        public static List<Requisite> MapRequisiteModel(IEnumerable<RequisiteDto>? requisites)
        {
            return requisites?
                            .Select(requisiteDto => Requisite.Create(requisiteDto.Name,
                                requisiteDto.AccountNumber,
                                requisiteDto.PaymentInstruction))
                            .Where(createResult => createResult.IsSuccess)
                            .Select(createResult => createResult.Value)
                            .ToList() ?? [];
        }

        public static List<SocialMediaLink> MapSocialMediaLinkModel(CreateVolunteerCommand request)
        {
            return request.SocialMediaLinks?
                            .Select(linkDto => SocialMediaLink.Create(linkDto.Name, linkDto.Link))
                            .Where(createResult => createResult.IsSuccess)
                            .Select(createResult => createResult.Value)
                            .ToList() ?? [];
        }

        public static List<SocialMediaLink> MapSocialMediaLinkModel(IEnumerable<SocialMediaLinkDto>? links)
        {
            return links?
                .Select(linkDto => SocialMediaLink.Create(linkDto.Name, linkDto.Link))
                .Where(createResult => createResult.IsSuccess)
                .Select(createResult => createResult.Value)
                .ToList() ?? [];
        }


    }
}