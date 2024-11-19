using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Application.Interfaces;

public interface ITokenProvider
{
    string GetAccessToken(User user);
}