using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.VolunteeringApplications.Domain;

public class VolunteeringApplication : Entity<VolunteeringApplicationId>
{
    // EF Core
    private VolunteeringApplication(VolunteeringApplicationId id) : base(id)
    {
    }

    private VolunteeringApplication(
        VolunteeringApplicationId id,
        Guid userId,
        string volunteerInfo,
        VolunteeringApplicationStatus status,
        DateTime creationDate,
        string? rejectionComment,
        Guid? discussionId)
        : base(id)
    {
        UserId = userId;
        VolunteerInfo = volunteerInfo;
        Status = status;
        CreatedAt = creationDate;
        RejectionComment = rejectionComment;
        DiscussionId = discussionId;
    }

    public Guid? AdminId { get; private set; }
    public Guid UserId { get; private set; }
    public string VolunteerInfo { get; private set; } = string.Empty;
    public VolunteeringApplicationStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string? RejectionComment { get; private set; }
    public Guid? DiscussionId { get; private set; }

    public static Result<VolunteeringApplication> CreateNewApplication(
        Guid userId,
        string volunteerInfo)
    {
        if (userId == Guid.Empty)
            return Errors.General.ValueIsInvalid("adminId or userId").ToErrorList();

        return new VolunteeringApplication(
            VolunteeringApplicationId.NewId(),
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

        if (string.IsNullOrWhiteSpace(comment))
            return Errors.General.ValueIsInvalid("comment").ToErrorList();

        Status = VolunteeringApplicationStatus.RevisionRequested;
        RejectionComment = comment;

        return Result.Success();
    }

    public Result FinalReject(string comment)
    {
        var result = CheckThatStatusAllowChanges();
        if (result.IsSuccess)
            return result;

        if (string.IsNullOrWhiteSpace(comment))
            return Errors.General.ValueIsInvalid("comment").ToErrorList();

        Status = VolunteeringApplicationStatus.Rejected;
        RejectionComment = comment;
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
        if (Status == VolunteeringApplicationStatus.Approved ||
            Status == VolunteeringApplicationStatus.Rejected)
            return Errors.VolunteeringApplications.ChangeStatusNotAllowed().ToErrorList();
        return Result.Success();
    }

    public void AssignAdmin(Guid adminId)
    {
        AdminId = adminId;
    }

    public void UnassignAdmin()
    {
        AdminId = null;
    }

    public Result Update(string volunteerInfo)
    {
        var result = CheckThatStatusAllowChanges();
        if (result.IsSuccess)
            return result;

        if (string.IsNullOrWhiteSpace(volunteerInfo))
            return Errors.General.ValueIsInvalid("volunteerInfo").ToErrorList();
        VolunteerInfo = volunteerInfo.Trim();

        return Result.Success();
    }
}