namespace PetFam.Domain.Shared
{
    public interface ISoftDeletable
    {
        void Delete();
        void Restore();
    }
}
