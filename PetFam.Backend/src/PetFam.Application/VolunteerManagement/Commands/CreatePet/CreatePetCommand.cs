﻿using PetFam.Application.Dtos.ValueObjects;
using PetFam.Shared.Abstractions;

namespace PetFam.Application.VolunteerManagement.Commands.CreatePet
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
