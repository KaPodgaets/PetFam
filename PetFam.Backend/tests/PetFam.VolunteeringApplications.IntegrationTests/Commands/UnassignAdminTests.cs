using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Commands.UnassignAdmin;

namespace PetFam.VolunteeringApplications.IntegrationTests.Commands;

public class UnassignAdminTests : ApplicationsTestBase
{
    public UnassignAdminTests(TestsWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task UnassignAdmin_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationWithAssignedAdminAsync();
        var command = new UnassignAdminCommand(applicationId);
        var sut = Scope.ServiceProvider.GetRequiredService<UnassignAdminHandler>();

        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var applications = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        applications.Should().NotBeNull();
        applications.Should().HaveCount(1);
        applications.First().Id.Value.Should().Be(applicationId);
        applications.First().AdminId.Should().BeNull();
    }
}