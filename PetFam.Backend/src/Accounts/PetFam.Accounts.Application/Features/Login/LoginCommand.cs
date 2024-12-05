using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.Features.Login;

public record LoginCommand(string Email, string Password):ICommand;