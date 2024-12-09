using FluentAssertions;
using PetFam.Discussions.Domain;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.UnitTests;

public static class DiscussionTestsHelper
{
    public static Discussion CreateDummyDiscussion()
    {
        var users = new List<User>
        {
            new User { Name = "John Doe", UserId = Guid.NewGuid() },
            new User { Name = "Anna Frank", UserId = Guid.NewGuid() }
        };

        return Discussion.Create(Guid.NewGuid(),users)
            .Value;
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
    public void RejectWithComment()
    {
        // Arrange
        var application = VolunteeringApplicationHelper.CreateDummyVolunteeringApplication();

        // Act
        var result = application.RejectWithComment("This is a comment");

        // Assert
        result.IsSuccess.Should().BeTrue();

        application.Status.Should().Be(VolunteeringApplicationStatus.RevisionRequested);
        application.RejectionComment.Should().NotBeNull();
        application.RejectionComment.Should().NotBeEmpty();
    }

    [Fact]
    public void RejectWithoutComment_should_fail()
    {
        // Arrange
        var application = VolunteeringApplicationHelper.CreateDummyVolunteeringApplication();

        // Act
        var result = application.RejectWithComment("  ");

        // Assert
        result.IsSuccess.Should().BeFalse();

        application.Status.Should().Be(VolunteeringApplicationStatus.Submitted);
        application.RejectionComment.Should().BeNull();
    }

    [Fact]
    public void FinalReject()
    {
        // Arrange
        var application = VolunteeringApplicationHelper.CreateDummyVolunteeringApplication();

        // Act
        application.FinalReject();

        // Assert
        application.Status.Should().Be(VolunteeringApplicationStatus.Rejected);
        application.RejectionComment.Should().BeNull();
    }

    [Fact]
    public void Approve()
    {
        // Arrange
        var application = VolunteeringApplicationHelper.CreateDummyVolunteeringApplication();

        // Act
        application.Approve();

        // Assert
        application.Status.Should().Be(VolunteeringApplicationStatus.Approved);
        application.RejectionComment.Should().BeNull();
    }
}