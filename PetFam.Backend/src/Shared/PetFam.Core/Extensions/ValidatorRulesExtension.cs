namespace PetFam.Shared.Extensions;

public static class ValidatorRulesExtension
{
    public static bool BeValidEnum<TEnum>(int enumValue)
    {
        return Enum.IsDefined(typeof(TEnum), enumValue);
    }
}