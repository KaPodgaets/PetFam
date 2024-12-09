namespace PetFam.Discussions.Domain;

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