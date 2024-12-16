using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Application.Commands.AddMessage;

public record AddMessageCommand(
    Guid DiscussionId,
    Guid UserId,
    string MessageText):ICommand;