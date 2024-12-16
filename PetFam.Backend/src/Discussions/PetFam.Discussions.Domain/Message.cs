using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Domain;

public class Message: Entity<MessageId>
{
    private Message(
        MessageId id,
        string text,
        DateTime createdAt,
        bool isEdited,
        Guid userId) : base(id)
    {
        Text = text;
        CreatedAt = createdAt;
        IsEdited = isEdited;
        UserId = userId;
    }

    public string Text { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsEdited { get; private set; }
    public Guid UserId { get; private set; }
    // public Discussion Discussion { get; private set; }
    // public Guid DiscussionId { get; private set; }
    public static Result<Message> Create(string text, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Errors.Messages.CannotBeEmptyOrWhitespace().ToErrorList();
        
        if(userId.Equals(Guid.Empty))
            return Errors.Messages.ShouldHaveUserId().ToErrorList();
                
        return new Message(MessageId.NewId(), text, DateTime.UtcNow, isEdited: false, userId);
    }
    public Result Edit(string newText)
    {
        if (string.IsNullOrWhiteSpace(newText))
            return Errors.Messages.CannotBeEmptyOrWhitespace().ToErrorList();
        
        Text = newText;
        IsEdited = true;
        return Result.Success();
    }
}