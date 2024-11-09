﻿namespace PetFam.Application.Dtos.ValueObjects
{
    public record PetGeneralInfoDto(string Comment,
            string Color,
            double Weight,
            double Height,
            string PhoneNumber);
}
