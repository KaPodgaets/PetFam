using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.Dtos
{
    public class VolunteerDto
    {
        public Guid Id { get; init; }
        public string FullName { get; init; }
        public string Email { get; init; }
        public GeneralInformationDto GeneralInformation1 { get; init; }
        public int AgesOfExpirience { get; init; }
        public SocialMediaLinkDto[] SocialMediaDetails { get; init; }
        public RequisiteDto[] Requisites { get; init; }
        public PetDto[] Pets { get; init; }
    }
}
