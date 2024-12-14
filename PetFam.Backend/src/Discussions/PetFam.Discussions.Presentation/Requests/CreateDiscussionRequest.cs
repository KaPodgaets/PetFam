using PetFam.Discussions.Application.Commands.Create;
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
        var users = new List<User>
        {
            User.Create(FirstUserId, FirstUserName),
            User.Create(SecondUserId, SecondUserName)
        };

        return new CreateDiscussionCommand(
            RelatedObjectId,
            users);
    }
}