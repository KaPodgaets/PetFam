using PetFam.Discussions.Application.Commands.Create;
using PetFam.Discussions.Application.Dtos;
using PetFam.Discussions.Domain;

namespace PetFam.Discussions.Presentation.Requests;

public record CreateDiscussionRequest(
    Guid FirstUserId,
    string FirstUserName,
    Guid SecondUserId,
    string SecondUserName,
    Guid RelatedObjectId)
{
    public CreateDiscussionCommand ToCommand()
    {
        var users = new List<UserDto>
        {
            new UserDto(FirstUserId, FirstUserName),
            new UserDto(SecondUserId, SecondUserName)
        };

        return new CreateDiscussionCommand(
            RelatedObjectId,
            users);
    }
}