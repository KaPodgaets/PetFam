namespace PetFam.Shared.Abstractions
{
    public interface ISoftDeletable
    {
        void Delete();
        void Restore();
    }
}
