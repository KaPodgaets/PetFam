namespace PetFam.Discussions.Domain;

public record User
{
    private User(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;

    public static User Create(Guid userId, string name)
    {
        return new User(userId, name);
    }
}

public class UserDto
{
    public UserDto(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
    
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
}