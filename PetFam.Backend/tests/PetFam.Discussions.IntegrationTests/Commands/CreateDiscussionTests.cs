using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application.Commands.Create;

namespace PetFam.Discussions.IntegrationTests.Commands;

public class CreateDiscussionTests:DiscussionsTestBase
{
    public CreateDiscussionTests(DiscussionsTestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CreateDiscussion_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var command = Fixture.FakeCreateCommand();
        var sut = Scope.ServiceProvider.GetRequiredService<CreateDiscussionHandler>();
        
        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var discussions = await ReadDbContext.Discussions.ToListAsync(cancellationToken);
        discussions.Should().NotBeNull();
        discussions.Should().HaveCount(1);
        discussions.First().IsClosed.Should().BeFalse();
    }
}