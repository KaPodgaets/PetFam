using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

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

    public static Result<User> Create(Guid userId, string name)
    {
        if (string.IsNullOrEmpty(name))
            return Errors.General.ValueIsInvalid("user.name").ToErrorList();
        
        return new User(userId, name);
    }
}