using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Commands.CreateApplication;

namespace PetFam.VolunteeringApplications.IntegrationTests;

public class ApplicationsTests:ApplicationsTestBase
{
    protected ApplicationsTests(TestsWebAppFactory factory) : base(factory)
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
    }
    [Fact]
    public void AssignAdmin_should_succeed()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    [Fact]
    public void UnassignAdmin_should_succeed()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    [Fact]
    public void ApproveApplication_should_succeed()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    [Fact]
    public void RejectApplication_should_succeed()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    [Fact]
    public void RequestRevision_should_succeed()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    [Fact]
    public void StartReview_should_succeed()
    {
        // Arrange
        
        // Act
        
        // Assert
    }
    [Fact]
    public async Task UpdateApplication_should_succeed()
    {
        // Arrange
        var applicationId = await SeedApplication();
        
        // Act

        // Assert
    }
}