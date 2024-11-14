﻿namespace PetFam.Shared.Dtos.ValueObjects
{
    public record PetHealthInfoDto(
        string Comment,
        bool IsCastrated,
        DateTime BirthDate,
        bool IsVaccinated,
        int Age);
}