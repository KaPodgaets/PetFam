using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Domain;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests;

public class GetVolunteerTest : IClassFixture<Factory>, IAsyncLifetime
{
    private readonly Factory _factory;
    private readonly IServiceScope _scope;
    private readonly WriteDbContext _writeDbContext;

    public GetVolunteerTest(Factory factory)
    {
        _factory = factory;
        _scope = factory.Services.CreateScope();
        _writeDbContext = _scope.ServiceProvider.GetRequiredService<WriteDbContext>();
    }

    [Fact]
    public void Test1()
    {
        _writeDbContext.Database.EnsureCreated();
        
        CreateVolunteerRecord();

        var volunteers = _writeDbContext.Volunteers.ToList();
        
        volunteers.Should().NotBeEmpty();
    }

    private void CreateVolunteerRecord()
    {
        var volunteer = Volunteer.Create(
            VolunteerId.NewId(),
            FullName.Create("Test", "Test", null).Value,
            Email.Create("Test@Test.com").Value,
            null,
            null
        ).Value;
        _writeDbContext.Add(volunteer);
        _writeDbContext.SaveChanges();
    }

    public async Task InitializeAsync()
    {
        await Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        _scope.Dispose();
        await Task.CompletedTask;
    }
}