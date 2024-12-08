using PetFam.Shared.SharedKernel;

namespace PetFam.Discussions.Domain;

public class Discussion:Entity<DiscussionId>
{
    private Discussion(DiscussionId id) : base(id)
    {
    }
    
    public Guid RelationId { get; set; }
    public List<User> Users { get; set; } = [];
    public List<Message> Messages { get; set; } = [];
}

public record DiscussionId
{
    private DiscussionId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; init; }
    public static DiscussionId NewId() => new(Guid.NewGuid());
    public static DiscussionId Empty() => new(Guid.Empty);
    public static DiscussionId Create(Guid id) => new(id);
}

public record User
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
}