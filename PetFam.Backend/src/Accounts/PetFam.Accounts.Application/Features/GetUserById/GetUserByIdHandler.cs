using PetFam.Accounts.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Features.GetUserById;

public class GetUserByIdHandler
    :IQueryHandler<User, GetUserByIdQuery>
{
    public Task<Result<User>> HandleAsync(
        GetUserByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}