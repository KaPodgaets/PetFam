namespace PetFam.VolunteeringApplications.Domain;

public record VolunteeringApplicationId
{
    private VolunteeringApplicationId()
    {
    }

    private VolunteeringApplicationId(Guid id)
    {
        Value = id;
    }

    public Guid Value { get; }
    public static VolunteeringApplicationId NewId() => new(Guid.NewGuid());
    public static VolunteeringApplicationId Empty() => new(Guid.Empty);
    public static VolunteeringApplicationId Create(Guid id) => new(id);
}