using PetFam.Discussions.Application.Dtos;
using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Application.Commands.Create;

public record CreateDiscussionCommand(
    Guid RelationId,
    List<UserDto> Users):ICommand;