namespace PetFam.Discussions.Domain;

public record User
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
}