using PetFam.Accounts.Application.Database;
using PetFam.Accounts.Application.Interfaces;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.DbContexts;

namespace PetFam.Accounts.Infrastructure.IdentityManagers;

public class ParticipantAccountsManager(AccountsWriteDbContext accountsContext):IParticipantAccountsManager
{
    public async Task CreateAccount(ParticipantAccount account,CancellationToken cancellationToken = default)
    {
        await accountsContext.ParticipantAccounts.AddAsync(account, cancellationToken);
        await accountsContext.SaveChangesAsync(cancellationToken);
    }
}