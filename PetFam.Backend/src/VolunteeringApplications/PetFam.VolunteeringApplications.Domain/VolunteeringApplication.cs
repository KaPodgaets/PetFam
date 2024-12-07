using System.Runtime.InteropServices.JavaScript;
using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Domain;

public class VolunteeringApplication : Entity<VolunteeringApplicationId>
{
    private VolunteeringApplication(
        VolunteeringApplicationId id,
        Guid adminId,
        Guid userId,
        string volunteerInfo,
        VolunteeringApplicationStatus status,
        DateTime creationDate,
        string? rejectionComment,
        Guid? discussionId) 
            : base(id)
    {
        AdminId = adminId;
        UserId = userId;
        VolunteerInfo = volunteerInfo;
        Status = status;
        CreatedAt = creationDate;
        RejectionComment = rejectionComment;
        DiscussionId = discussionId;
    }

    public Guid AdminId { get; set; }
    public Guid UserId { get; set; }
    public string VolunteerInfo { get; set; } = string.Empty;
    public VolunteeringApplicationStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? RejectionComment { get; set; }
    public Guid? DiscussionId { get; set; }

    public static Result<VolunteeringApplication> CreateNewApplication(
        Guid adminId,
        Guid userId,
        string volunteerInfo)
    {
        if (adminId == Guid.Empty || userId == Guid.Empty)
            return Errors.General.ValueIsInvalid("adminId or userId").ToErrorList();
        
        return new VolunteeringApplication(
            VolunteeringApplicationId.NewId(),
            adminId,
            userId,
            volunteerInfo,
            VolunteeringApplicationStatus.Submitted,
            DateTime.UtcNow,
            null,
            null
        );
    }

    public void StartReview()
    {
        Status = VolunteeringApplicationStatus.Review;
    }

    public Result RejectWithComment(string comment)
    {
        if(string.IsNullOrWhiteSpace(comment))
            return Errors.General.ValueIsInvalid("comment").ToErrorList();
        
        Status = VolunteeringApplicationStatus.RevisionRequested;
        RejectionComment = comment;
        
        return Result.Success();
    }
    
    public void FinalReject()
    {
        Status = VolunteeringApplicationStatus.Rejected;
        RejectionComment = null;
    }
    
    public void Approve()
    {
        Status = VolunteeringApplicationStatus.Approved;
        RejectionComment = null;
    }
}