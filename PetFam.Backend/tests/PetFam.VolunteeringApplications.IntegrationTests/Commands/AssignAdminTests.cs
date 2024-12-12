using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;

namespace PetFam.VolunteeringApplications.IntegrationTests.Commands;

public class AssignAdminTests : ApplicationsTestBase
{
    public AssignAdminTests(TestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AssignAdmin_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationAsync();
        var command = new AssignAdminCommand(applicationId, Guid.NewGuid());
        var sut = Scope.ServiceProvider.GetRequiredService<AssignAdminHandler>();

        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var applications = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        applications.Should().NotBeNull();
        applications.Should().HaveCount(1);
        applications.First().Id.Value.Should().Be(applicationId);
        applications.First().AdminId.Should().Be(command.AdminId);
    }
}