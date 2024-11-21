using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Application.Interfaces;

public interface ITokenProvider
{
    Task<string> GetAccessToken(User user, CancellationToken cancellationToken = default);
}