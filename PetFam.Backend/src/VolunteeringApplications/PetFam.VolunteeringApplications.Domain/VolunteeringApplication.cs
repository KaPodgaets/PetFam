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

    public Guid AdminId { get; private set; }
    public Guid UserId { get; private set; }
    public string VolunteerInfo { get; private set; }
    public VolunteeringApplicationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? RejectionComment { get; private set; }
    public Guid? DiscussionId { get; private set; }

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

    public Result StartReview()
    {
        var result = CheckThatStatusAllowChanges();
        if (result.IsSuccess)
            return result;
        
        Status = VolunteeringApplicationStatus.Review;
        
        return Result.Success();
    }

    public Result RejectWithComment(string comment)
    {
        var result = CheckThatStatusAllowChanges();
        if (result.IsSuccess)
            return result;
        
        if(string.IsNullOrWhiteSpace(comment))
            return Errors.General.ValueIsInvalid("comment").ToErrorList();
        
        Status = VolunteeringApplicationStatus.RevisionRequested;
        RejectionComment = comment;
        
        return Result.Success();
    }
    
    public Result FinalReject()
    {
        var result = CheckThatStatusAllowChanges();
        if (result.IsSuccess)
            return result;
        Status = VolunteeringApplicationStatus.Rejected;
        RejectionComment = null;
        return Result.Success();
    }
    
    public Result Approve()
    {
        var result = CheckThatStatusAllowChanges();
        if (result.IsSuccess)
            return result;
        Status = VolunteeringApplicationStatus.Approved;
        RejectionComment = null;
        return Result.Success();
    }

    private Result CheckThatStatusAllowChanges()
    {
        if(Status == VolunteeringApplicationStatus.Approved ||
           Status == VolunteeringApplicationStatus.Rejected)
            return Errors.VolunteeringApplications.ChangeStatusNotAllowed().ToErrorList();
        return Result.Success();
    }
}