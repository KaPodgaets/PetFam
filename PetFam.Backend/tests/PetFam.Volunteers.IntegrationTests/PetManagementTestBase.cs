using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Domain;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests;

public class PetManagementTestBase : IClassFixture<TestsWebAppFactory>, IAsyncLifetime
{
    protected readonly Fixture _fixture;
    protected readonly TestsWebAppFactory _factory;
    protected readonly IServiceScope _scope;
    protected readonly WriteDbContext _writeDbContext;


    protected PetManagementTestBase(TestsWebAppFactory factory)
    {
        _factory = factory;
        _fixture = new Fixture();
        _scope = factory.Services.CreateScope();
        _writeDbContext = _scope.ServiceProvider.GetRequiredService<WriteDbContext>();
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