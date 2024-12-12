namespace PetFam.Discussions.Domain;

public record Users
{
    public Users(User first, User second)
    {
        FirstUser = first;
        SecondUser = second;
    }
    public User FirstUser { get; init; }
    public User SecondUser { get; init; }
}