namespace PetFam.Discussions.Domain;

public record MessageId
{
    private MessageId(Guid id)
    {
        Value = id;
    }
    public Guid Value { get; init; }
    public static MessageId NewId() => new(Guid.NewGuid());
    public static MessageId Empty() => new(Guid.Empty);
    public static MessageId Create(Guid id) => new(id);
}