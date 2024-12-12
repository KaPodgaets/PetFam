using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Commands.Approve;
using PetFam.VolunteeringApplications.Application.Commands.Shared;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.IntegrationTests;

public class ApproveApplicationTests : ApplicationsTestBase
{
    public ApproveApplicationTests(TestsWebAppFactory factory) : base(factory)
    {
    }
    [Fact]
    public async Task ApproveApplication_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationAsync();
        var command = new ChangeApplicationStatusCommand(applicationId);
        var sut = Scope.ServiceProvider.GetRequiredService<ApproveHandler>();

        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var applications = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        applications.Should().NotBeNull();
        applications.Should().HaveCount(1);
        applications.First().Id.Value.Should().Be(applicationId);
        applications.First().Status.Should().Be(VolunteeringApplicationStatus.Approved);
    }
}