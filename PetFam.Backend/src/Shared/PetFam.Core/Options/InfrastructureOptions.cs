namespace PetFam.Shared.Options
{
    public static class InfrastructureOptions
    {
        public const string DATABASE = "Database";
    }

    public class SoftDeleteOptions
    {
        public const string SectionName = nameof(SoftDeleteOptions);
        public int DaysBeforeRemove { get; init; }
    }
}
