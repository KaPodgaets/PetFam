using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.Create;
using PetFam.PetManagement.Domain;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.Volunteers.IntegrationTests;

public class AddVolunteerTest : IClassFixture<TestsWebAppFactory>, IAsyncLifetime
{
    private readonly Fixture _fixture;
    private readonly TestsWebAppFactory _factory;
    private readonly IServiceScope _scope;
    private readonly WriteDbContext _writeDbContext;
    private readonly ICommandHandler<Guid, CreateVolunteerCommand> _sut;

    public AddVolunteerTest(TestsWebAppFactory factory)
    {
        _factory = factory;
        _fixture = new Fixture();
        _scope = factory.Services.CreateScope();
        _writeDbContext = _scope.ServiceProvider.GetRequiredService<WriteDbContext>();
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, CreateVolunteerCommand>>();
    }

    [Fact]
    public async Task AddVolunteerToDatabase_should_be_success()
    {
        // Arrange
        // SeedVolunteer();
        var command = _fixture.FakeCreateVolunteerCommand();
        
        // Act
        var result = await _sut.ExecuteAsync(command);
            
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().Be(true);
        result.Value.Should().NotBeEmpty();
        
        var volunteers = _writeDbContext.Volunteers.ToList();
        volunteers.Should().NotBeEmpty();
        volunteers.Should().HaveCount(1);
    }

    private void SeedVolunteer()
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
        await _factory.ResetDatabaseAsync();
        await Task.CompletedTask;
    }
}