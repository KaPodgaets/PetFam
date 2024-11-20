using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.DbContexts;

namespace PetFam.Accounts.Infrastructure.IdentityManagers;

public class VolunteerAccountsManager(AccountsWriteDbContext accountsContext)
{
    public async Task CreateAccount(VolunteerAccount account,CancellationToken cancellationToken = default)
    {
        await accountsContext.VolunteerAccounts.AddAsync(account, cancellationToken);
        await accountsContext.SaveChangesAsync(cancellationToken);
    }
}