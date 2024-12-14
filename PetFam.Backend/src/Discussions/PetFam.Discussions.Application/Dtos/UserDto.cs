namespace PetFam.Discussions.Application.Dtos;

public class UserDto
{
    public UserDto(Guid userId, string name)
    {
        UserId = userId;
        Name = name;
    }
    
    public Guid UserId { get; init; }
    public string Name { get; init; }
}