using FluentAssertions;
using PetFam.Discussions.Domain;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.UnitTests;

public static class DiscussionTestsHelper
{
    public const string InitTextForMessage = "This is a test message";
    public static Discussion CreateDummyDiscussion()
    {
        var users = new List<User>
        {
           User.Create(Guid.NewGuid(), Guid.NewGuid().ToString()),
           User.Create(Guid.NewGuid(), Guid.NewGuid().ToString()),
        };

        return Discussion.Create(Guid.NewGuid(),users)
            .Value;
    }

    public static Discussion AddMessage(this Discussion discussion)
    {
        var message = Message.Create(DiscussionTestsHelper.InitTextForMessage, discussion.Users[0].UserId).Value;
        discussion.AddMessage(message);
        return discussion;
    }
}

public class DiscussionTests
{
    [Fact]
    public void AddMessageToDiscussion_should_success()
    {
        // Arrange
        var discussion = DiscussionTestsHelper.CreateDummyDiscussion();
        var message = Message.Create("test message", discussion.Users[0].UserId).Value;
        
        // Act
        var result = discussion.AddMessage(message);

        // Assert
        result.IsSuccess.Should().Be(true);
        discussion.Messages.Should().HaveCount(1);
    }
    
    [Fact]
    public void CreateNewMessageWithEmptyText_should_fail()
    {
        // Arrange
        
        // Act
        var result = Message.Create("   ", Guid.NewGuid());

        // Assert
        result.IsSuccess.Should().Be(false);
    }

    [Fact]
    public void DeleteMessage_should_success()
    {
        // Arrange
        var discussion = DiscussionTestsHelper.CreateDummyDiscussion().AddMessage();
        var messageId = discussion.Messages[0].Id.Value;
        var userId = discussion.Messages[0].UserId;
        
        // Act
        var result = discussion.DeleteMessage(messageId, userId);

        // Assert
        result.IsSuccess.Should().Be(true);
        discussion.Messages.Should().BeEmpty();
    }
    
    [Fact]
    public void DeleteMessageWithNonParticipantUserId_should_fail()
    {
        // Arrange
        var discussion = DiscussionTestsHelper.CreateDummyDiscussion().AddMessage();
        var messageId = discussion.Messages[0].Id.Value;
        var userId = Guid.NewGuid();
        
        // Act
        var result = discussion.DeleteMessage(messageId, userId);

        // Assert
        result.IsSuccess.Should().Be(false);
        discussion.Messages.Should().HaveCount(1);
    }

    [Fact]
    public void EditMessage_should_success()
    {
        // Arrange
        var discussion = DiscussionTestsHelper.CreateDummyDiscussion().AddMessage();
        var messageId = discussion.Messages[0].Id.Value;
        var userId = discussion.Messages[0].UserId;
        var newText = "new message";
        
        // Act
        var result = discussion.EditMessage(messageId, userId,newText);

        // Assert
        result.IsSuccess.Should().Be(true);
        discussion.Messages[0].Text.Should().Be(newText);
    }
    
    [Fact]
    public void EditMessageChangeTextToWhiteSpace_should_fail()
    {
        // Arrange
        var discussion = DiscussionTestsHelper.CreateDummyDiscussion().AddMessage();
        var messageId = discussion.Messages[0].Id.Value;
        var userId = discussion.Messages[0].UserId;
        
        // Act
        var result = discussion.EditMessage(messageId, userId,"   ");

        // Assert
        result.IsSuccess.Should().Be(false);
        discussion.Messages[0].Text.Should().Be(DiscussionTestsHelper.InitTextForMessage);
    }
}