using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.Features.RegisterUser;

public record RegisterUserCommand(string Email, string Password):ICommand;