namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.CreatePet
{
    public record CreatePetCommand(
        Guid VolunteerId,
        string NickName,
        string SpeciesName,
        string BreedName,
        PetGeneralInfoDto PetGeneralInfoDto,
        PetHealthInfoDto PetHealthInfoDto,
        PetAddressDto AddressDto,
        AccountInfoDto AccountInfoDto):ICommand;
}
