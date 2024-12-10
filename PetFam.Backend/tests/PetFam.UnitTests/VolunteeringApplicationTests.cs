using FluentAssertions;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.UnitTests;

public class VolunteeringApplicationHelper
{
    public static VolunteeringApplication CreateDummyVolunteeringApplication()
    {
        return VolunteeringApplication.CreateNewApplication(
            Guid.NewGuid(),
            "Test Volunteer Application"
            ).Value;
    }
}
public class VolunteeringApplicationTests
{
    [Fact]
    public void StartReview()
    {
        // Arrange
        var application = VolunteeringApplicationHelper.CreateDummyVolunteeringApplication();
        
        // Act
        application.StartReview();

        // Assert
        application.Status.Should().Be(VolunteeringApplicationStatus.Review);
    }

    [Fact]
    public void RejectWithComment()
    {
        // Arrange
        var application = VolunteeringApplicationHelper.CreateDummyVolunteeringApplication();
        
        // Act
        var result = application.RequestRevision("This is a comment");
        
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
        var result = application.RequestRevision("  ");
        
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
        application.FinalReject("test comments");

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