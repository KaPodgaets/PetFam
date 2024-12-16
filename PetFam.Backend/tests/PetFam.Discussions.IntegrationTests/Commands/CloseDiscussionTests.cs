using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application.Commands.Close;

namespace PetFam.Discussions.IntegrationTests.Commands;

public class CloseDiscussionTests:DiscussionsTestBase
{
    public CloseDiscussionTests(DiscussionsTestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task CloseDiscussion_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var discussion = await SeedDiscussion();
        var command = Fixture.FakeCloseDiscussionCommand(discussion.Id.Value);
        var sut = Scope.ServiceProvider.GetRequiredService<CloseDiscussionHandler>();
        
        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var discussions = await ReadDbContext.Discussions.ToListAsync(cancellationToken);
        discussions.Should().NotBeNull();
        discussions.Should().HaveCount(1);
        discussions.First().IsClosed.Should().BeTrue();
    }
}