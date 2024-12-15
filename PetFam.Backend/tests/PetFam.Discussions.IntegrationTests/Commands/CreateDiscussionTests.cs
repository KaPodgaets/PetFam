using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetFam.Discussions.Application.Commands.AddMessage;
using PetFam.Discussions.Application.Commands.Close;
using PetFam.Discussions.Application.Commands.Create;
using PetFam.Discussions.Application.Commands.DeleteMessage;
using PetFam.Discussions.Application.Commands.EditeMessage;

namespace PetFam.Discussions.IntegrationTests.Commands;

public class CreateDiscussionTests:DiscussionsTestBase
{
    protected CreateDiscussionTests(DiscussionsTestsWebAppFactory factory) : base(factory)
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

public class CloseDiscussionTests:DiscussionsTestBase
{
    protected CloseDiscussionTests(DiscussionsTestsWebAppFactory factory) : base(factory)
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

public class AddMessageTests:DiscussionsTestBase
{
    protected AddMessageTests(DiscussionsTestsWebAppFactory factory) : base(factory)
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
        var discussions = await ReadDbContext.Discussions.ToListAsync(cancellationToken);
        discussions.Should().NotBeNull();
        discussions.Should().HaveCount(1);
        discussions.First().Messages.Should().HaveCount(1);
        discussions.First().Messages[0].UserId.Should().Be(discussion.Users[0].UserId);
        discussions.First().Messages[0].Text.Should().Be(command.MessageText);
        discussions.First().IsClosed.Should().BeFalse();
    }
}

public class EditMessageTests:DiscussionsTestBase
{
    protected EditMessageTests(DiscussionsTestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task EditMessage_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var discussion = await SeedDiscussion();
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
        var discussions = await ReadDbContext.Discussions.ToListAsync(cancellationToken);
        discussions.Should().NotBeNull();
        discussions.Should().HaveCount(1);
        discussions.First().Messages.Should().HaveCount(1);
        discussions.First().Messages[0].UserId.Should().Be(discussion.Users[0].UserId);
        discussions.First().Messages[0].Text.Should().Be(command.NewText);
        discussions.First().IsClosed.Should().BeFalse();
    }
}

public class DeleteMessageTests:DiscussionsTestBase
{
    protected DeleteMessageTests(DiscussionsTestsWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task DeleteMessage_should_succeed()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var discussion = await SeedDiscussion();
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
        var discussions = await ReadDbContext.Discussions.ToListAsync(cancellationToken);
        discussions.Should().NotBeNull();
        discussions.Should().HaveCount(1);
        discussions.First().Messages.Should().BeEmpty();
    }
}