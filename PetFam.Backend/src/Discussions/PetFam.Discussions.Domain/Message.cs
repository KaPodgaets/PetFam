using PetFam.Shared.SharedKernel;

namespace PetFam.Discussions.Domain;

public class Message: Entity<MessageId>
{
    private Message(MessageId id) : base(id)
    {
    }

    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsEdited { get; set; }
    public Guid UserId { get; set; }
}