using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application.Commands.AddMessage;

namespace PetFam.Discussions.IntegrationTests.Commands;

public class AddMessageTests:DiscussionsTestBase
{
    public AddMessageTests(DiscussionsTestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task AddMessage_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var discussion = await SeedDiscussion();
        var command = Fixture.FakeAddMessageCommand(discussion.Id.Value, discussion.Users[0].UserId);
        var sut = Scope.ServiceProvider.GetRequiredService<AddMessageHandler>();
        
        // Act
        var result = await sut.ExecuteAsync(command, cancellationToken);
        
        // Assert
        result.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
        var discussions = await ReadDbContext
            .Discussions
            .Include(x => x.Messages)
            .ToListAsync(cancellationToken);
        discussions.Should().NotBeNull();
        discussions.Should().HaveCount(1);
        discussions.First().Messages.Should().HaveCount(1);
        discussions.First().Messages[0].UserId.Should().Be(discussion.Users[0].UserId);
        discussions.First().Messages[0].Text.Should().Be(command.MessageText);
        discussions.First().IsClosed.Should().BeFalse();
    }
}