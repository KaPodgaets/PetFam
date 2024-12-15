using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application.Commands.DeleteMessage;

namespace PetFam.Discussions.IntegrationTests.Commands;

public class DeleteMessageTests:DiscussionsTestBase
{
    public DeleteMessageTests(DiscussionsTestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task DeleteMessage_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var discussion = await SeedDiscussionWithMessagesAsync();
        var command = Fixture.FakeDeleteMessageCommand(
            discussion.Id.Value,
            discussion.Messages[0].Id.Value,
            discussion.Users[0].UserId);
        var sut = Scope.ServiceProvider.GetRequiredService<DeleteMessageHandler>();
        
        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var discussions = await ReadDbContext.Discussions
            .Include(x => x.Messages)
            .ToListAsync(cancellationToken);
        discussions.Should().NotBeNull();
        discussions.Should().HaveCount(1);
        discussions.First().Messages.Should().BeEmpty();
    }
}