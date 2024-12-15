using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Application.Commands.EditeMessage;

public record EditMessageCommand(
    Guid DiscussionId,
    Guid MessageId,
    Guid UserId,
    string NewText):ICommand;