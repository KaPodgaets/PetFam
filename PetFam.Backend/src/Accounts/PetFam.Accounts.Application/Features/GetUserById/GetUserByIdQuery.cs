using PetFam.Shared.Abstractions;

namespace PetFam.Accounts.Application.Features.GetUserById;

public record GetUserByIdQuery(Guid UserId):IQuery;