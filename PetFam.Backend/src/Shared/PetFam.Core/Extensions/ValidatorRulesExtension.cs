namespace PetFam.Shared.Extensions;

public static class ValidatorRulesExtension
{
    public static bool BeValidPetStatus<TEnum>(int enumValue)
    {
        return Enum.IsDefined(typeof(TEnum), enumValue);
    }
}