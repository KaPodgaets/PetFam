using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.Create;
using PetFam.Shared.Abstractions;

namespace PetFam.Volunteers.IntegrationTests.Volunteers;

public class AddVolunteerTest : PetManagementTestBase
{
    private readonly ICommandHandler<Guid, CreateVolunteerCommand> _sut;

    public AddVolunteerTest(TestsWebAppFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, CreateVolunteerCommand>>();
    }

    [Fact]
    public async Task AddVolunteerToDatabase_should_be_success()
    {
        // Arrange
        var command = _fixture.FakeCreateVolunteerCommand();

        // Act
        var result = await _sut.ExecuteAsync(command);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().Be(true);
        result.Value.Should().NotBeEmpty();

        var volunteers = VolunteersWriteDbContext.Volunteers.ToList();
        volunteers.Should().NotBeEmpty();
        volunteers.Should().HaveCount(1);
    }
}