using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.Login;

public record LoginCommand(string Email, string Password):ICommand;