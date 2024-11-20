using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Application.RegisterUser;

public interface IParticipantAccountsManager
{
    Task CreateAccount(ParticipantAccount account, CancellationToken cancellationToken = default);
}