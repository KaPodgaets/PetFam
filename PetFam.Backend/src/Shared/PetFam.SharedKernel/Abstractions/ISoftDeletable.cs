namespace PetFam.Shared.SharedKernel.Abstractions;

public interface ISoftDeletable
{
    void Delete();
    void Restore();
}

public abstract class SoftDeletableEntity : ISoftDeletable
{
    public bool IsDeleted { get; set; }
    public DateTime DeletedOn { get; set; }
    public virtual void Delete()
    {
        IsDeleted = true;
    }

    public virtual void Restore()
    {
        IsDeleted = false;
    }
}