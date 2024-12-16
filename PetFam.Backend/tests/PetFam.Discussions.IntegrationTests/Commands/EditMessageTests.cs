using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application.Commands.EditeMessage;

namespace PetFam.Discussions.IntegrationTests.Commands;

public class EditMessageTests:DiscussionsTestBase
{
    public EditMessageTests(DiscussionsTestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task EditMessage_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var discussion = await SeedDiscussionWithMessagesAsync();
        var command = Fixture.FakeEditMessageCommand(
            discussion.Id.Value,
            discussion.Messages[0].Id.Value,
            discussion.Users[0].UserId);
        var sut = Scope.ServiceProvider.GetRequiredService<EditMessageHandler>();
        
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
        discussions.First().Messages.Should().HaveCount(1);
        discussions.First().Messages[0].UserId.Should().Be(discussion.Users[0].UserId);
        discussions.First().Messages[0].Text.Should().Be(command.NewText);
        discussions.First().IsClosed.Should().BeFalse();
    }
}