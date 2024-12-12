using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Commands.Update;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.IntegrationTests.Commands;

public class UpdateApplicationTests : ApplicationsTestBase
{
    public UpdateApplicationTests(TestsWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task UpdateApplication_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationAsync();
        var command = new UpdateCommand(applicationId, Fixture.Create<string>());
        var sut = Scope.ServiceProvider.GetRequiredService<UpdateHandler>();

        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var applications = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        applications.Should().NotBeNull();
        applications.Should().HaveCount(1);
        applications.First().Id.Value.Should().Be(applicationId);
        applications.First().VolunteerInfo.Should().Be(command.VolunteerInfo);
        applications.First().Status.Should().Be(VolunteeringApplicationStatus.Submitted);
    }
}