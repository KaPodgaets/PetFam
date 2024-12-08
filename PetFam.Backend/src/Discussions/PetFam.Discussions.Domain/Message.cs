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
    public DateTime CreatedAt { get; set; }
    public bool IsEdited { get; private set; }

    public Guid UserId { get; set; }

    public static Result<Message> Create(string text, Guid userId)
    {
        if (string.IsNullOrEmpty(text))
            return Errors.Messages.CannotBeEmptyOrWhitespace().ToErrorList();
                
        return new Message(MessageId.NewId(), text, DateTime.UtcNow, isEdited: false, userId);
    }
    public Result Edit(string newText)
    {
        if (string.IsNullOrEmpty(newText))
            return Errors.Messages.CannotBeEmptyOrWhitespace().ToErrorList();
        
        Text = newText;
        IsEdited = true;
        return Result.Success();
    }
}