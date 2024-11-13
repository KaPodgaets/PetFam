namespace PetFam.Application.Exte;

public static class ValidatorExtension
{
    public static bool BeValidPetStatus<TEnum>(int enumValue)
    {
        return Enum.IsDefined(typeof(TEnum), enumValue);
    }
}