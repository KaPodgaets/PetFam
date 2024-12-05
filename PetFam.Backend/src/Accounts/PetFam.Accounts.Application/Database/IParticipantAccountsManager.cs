using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Application.Database;

public interface IParticipantAccountsManager
{
    Task CreateAccount(ParticipantAccount account, CancellationToken cancellationToken = default);
}