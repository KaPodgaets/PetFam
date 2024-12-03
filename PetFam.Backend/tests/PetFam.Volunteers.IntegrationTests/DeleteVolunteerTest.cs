using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using PetFam.PetManagement.Application.VolunteerManagement.Commands.Delete;
using PetFam.Shared.Abstractions;

namespace PetFam.Volunteers.IntegrationTests;

public class DeleteVolunteerTest : PetManagementTestBase
{
    private readonly ICommandHandler<Guid, DeleteVolunteerCommand> _sut;
    public DeleteVolunteerTest(TestsWebAppFactory factory) : base(factory)
    {
        _sut = _scope.ServiceProvider.GetRequiredService<ICommandHandler<Guid, DeleteVolunteerCommand>>();
    }
    
    [Fact]
    public async Task DeleteVolunteerFromDatabase_should_be_success()
    {
        // Arrange
        var volunteerId = await SeedVolunteer();
        var command = _fixture.FakeDeleteVolunteerCommand(volunteerId);

        // Act
        var result = await _sut.ExecuteAsync(command);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().Be(true);
        result.Value.Should().NotBeEmpty();
        result.Value.Should().Be(volunteerId);

        var volunteers = _writeDbContext.Volunteers.ToList();
        volunteers.Should().BeEmpty();
    }


}