using PetFam.Accounts.Domain;

namespace PetFam.Accounts.Application.Interfaces;

public interface IParticipantAccountsManager
{
    Task CreateAccount(ParticipantAccount account, CancellationToken cancellationToken = default);
}