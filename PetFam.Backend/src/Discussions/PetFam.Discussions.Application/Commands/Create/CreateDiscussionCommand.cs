
using PetFam.Discussions.Domain;
using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Application.Commands.Create;

public record CreateDiscussionCommand(
    Guid RelationId,
    List<User> Users):ICommand;