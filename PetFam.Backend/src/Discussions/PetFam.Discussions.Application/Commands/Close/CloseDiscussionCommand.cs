using PetFam.Shared.Abstractions;

namespace PetFam.Discussions.Application.Commands.Close;

public record CloseDiscussionCommand(
    Guid Id):ICommand;