using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.VolunteeringApplications.Application.Commands.Approve;
using PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;
using PetFam.VolunteeringApplications.Application.Commands.CreateApplication;
using PetFam.VolunteeringApplications.Application.Commands.Reject;
using PetFam.VolunteeringApplications.Application.Commands.RequestRevision;
using PetFam.VolunteeringApplications.Application.Commands.Shared;
using PetFam.VolunteeringApplications.Application.Commands.UnassignAdmin;
using PetFam.VolunteeringApplications.Application.Commands.Update;
using PetFam.VolunteeringApplications.Application.Queries.Get;
using PetFam.VolunteeringApplications.Application.Queries.GetById;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.IntegrationTests;

public class ApplicationsTests:ApplicationsTestBase
{
    public ApplicationsTests(TestsWebAppFactory factory) : base(factory)
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
    
    [Fact]
    public async Task RejectApplication_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationAsync();
        var command = new RejectApplicationCommand(applicationId, Fixture.Create<string>());
        var sut = Scope.ServiceProvider.GetRequiredService<RejectHandler>();
        
        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var applications = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        applications.Should().NotBeNull();
        applications.Should().HaveCount(1);
        applications.First().Id.Value.Should().Be(applicationId);
        applications.First().Status.Should().Be(VolunteeringApplicationStatus.Rejected);
    }
    [Fact]
    public async Task RequestRevision_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationAsync();
        var command = new RejectApplicationCommand(applicationId, Fixture.Create<string>());
        var sut = Scope.ServiceProvider.GetRequiredService<RequestRevisionHandler>();
        
        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var applications = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        applications.Should().NotBeNull();
        applications.Should().HaveCount(1);
        applications.First().Id.Value.Should().Be(applicationId);
        applications.First().Status.Should().Be(VolunteeringApplicationStatus.RevisionRequested);
    }
    [Fact]
    public async Task StartReview_should_succeed()
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
        applications.First().Status.Should().Be(VolunteeringApplicationStatus.Review);
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
    [Fact]
    public async Task GetQueryWithPagination_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        await SeedFewApplicationsAsync(5);
        var applicationsToQuery = await ReadDbContext.Applications.ToListAsync(cancellationToken);
        var query = new GetWithPaginationQuery(applicationsToQuery[1].UserId, null, null, 1, 10);
        var sut = Scope.ServiceProvider.GetRequiredService<GetHandler>();
        
        // Act
        var result = await sut.HandleAsync(query, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        
        result.Value.Items.Should().HaveCount(1);
        result.Value.Items[0].Id.Should().Be(applicationsToQuery[1].Id);
    }
    [Fact]
    public async Task GetByIdQuery_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var applicationId = await SeedApplicationAsync();
        var query = new GetByIdQuery(applicationId);
        var sut = Scope.ServiceProvider.GetRequiredService<GetByIdHandler>();
        
        // Act
        var result = await sut.HandleAsync(query, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Value.Should().Be(applicationId);
    }
}