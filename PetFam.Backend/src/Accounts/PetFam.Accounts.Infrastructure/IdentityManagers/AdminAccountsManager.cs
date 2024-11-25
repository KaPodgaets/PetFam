using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.DbContexts;

namespace PetFam.Accounts.Infrastructure.IdentityManagers;

public class AdminAccountsManager(AccountsWriteDbContext accountsContext)
{
    public async Task CreateAccount(AdminAccount adminAccount,CancellationToken cancellationToken = default)
    {
        await accountsContext.AdminAccounts.AddAsync(adminAccount, cancellationToken);
        await accountsContext.SaveChangesAsync(cancellationToken);
    }
}