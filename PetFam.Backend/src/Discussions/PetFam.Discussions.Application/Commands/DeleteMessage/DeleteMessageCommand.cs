using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Application.Commands.DeleteMessage;

public record DeleteMessageCommand(
    Guid DiscussionId,
    Guid UserId,
    Guid MessageId):ICommand;