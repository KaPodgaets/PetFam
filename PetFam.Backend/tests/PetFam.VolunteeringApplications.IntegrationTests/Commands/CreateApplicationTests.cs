using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Commands.CreateApplication;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.IntegrationTests.Commands;

public class CreateApplicationTests:ApplicationsTestBase
{
    public CreateApplicationTests(TestsWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task CreateApplication_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var command = new CreateCommand(Guid.NewGuid(), Fixture.Create<string>());
        var sut = Scope.ServiceProvider.GetRequiredService<CreateApplicationHandler>();
        
        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var applications = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        applications.Should().NotBeNull();
        applications.Should().HaveCount(1);
        applications.First().Status.Should().Be(VolunteeringApplicationStatus.Submitted);
    }
}