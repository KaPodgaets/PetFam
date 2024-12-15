using PetFam.Discussions.Application.Commands.AddMessage;

namespace PetFam.Discussions.Presentation.Requests;

public record AddMessageRequest(Guid UserId, string MessageText)
{
    public AddMessageCommand ToCommand(Guid discussionId)
    {
        return new AddMessageCommand(discussionId, UserId, MessageText);
    }
}