using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Domain;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.PetManagement.Domain;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Accounts.IntegrationTests;

public class AccountsTestBase: IClassFixture<AccountsTestsWebAppFactory>, IAsyncLifetime
{
    protected readonly Fixture Fixture;
    protected readonly AccountsTestsWebAppFactory Factory;
    protected readonly IServiceScope Scope;
    protected readonly AccountsWriteDbContext WriteDbContext;


    protected AccountsTestBase(AccountsTestsWebAppFactory factory)
    {
        Factory = factory;
        Fixture = new Fixture();
        Scope = factory.Services.CreateScope();
        WriteDbContext = Scope.ServiceProvider.GetRequiredService<AccountsWriteDbContext>();
    }

    protected async Task<Guid> SeedAdminUser()
    {
        var adminUser = User.CreateAdmin(
            "Test@Test.com",
            []
        );
        
        await WriteDbContext.Users.AddAsync(adminUser);
        await WriteDbContext.SaveChangesAsync();
        
        return adminUser.Id;
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        Scope.Dispose();
        await Factory.ResetDatabaseAsync();
        await Task.CompletedTask;
    }
}