using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Accounts.Infrastructure.DbContexts;
using PetFam.PetManagement.Domain;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Accounts.IntegrationTests;

public class AccountsTestBase: IClassFixture<AccountsTestsWebAppFactory>, IAsyncLifetime
{
    protected readonly Fixture _fixture;
    protected readonly AccountsTestsWebAppFactory _factory;
    protected readonly IServiceScope _scope;
    protected readonly AccountsWriteDbContext _writeDbContext;


    protected AccountsTestBase(AccountsTestsWebAppFactory factory)
    {
        _factory = factory;
        _fixture = new Fixture();
        _scope = factory.Services.CreateScope();
        _writeDbContext = _scope.ServiceProvider.GetRequiredService<AccountsWriteDbContext>();
    }

    protected async Task<Guid> SeedVolunteer()
    {
        var volunteer = Volunteer.Create(
            VolunteerId.NewId(),
            FullName.Create("Test", "Test", null).Value,
            Email.Create("Test@Test.com").Value,
            null,
            null
        ).Value;
        
        await _writeDbContext.AddAsync(volunteer);
        await _writeDbContext.SaveChangesAsync();
        
        return volunteer.Id.Value;
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _scope.Dispose();
        await _factory.ResetDatabaseAsync();
        await Task.CompletedTask;
    }
}