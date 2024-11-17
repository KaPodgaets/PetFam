using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.RegisterUser;

public record RegisterUserCommand(string Email, string Password):ICommand;