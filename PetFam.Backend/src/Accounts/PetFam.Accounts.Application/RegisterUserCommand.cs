using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application;

public record RegisterUserCommand(string Email, string Password):ICommand;