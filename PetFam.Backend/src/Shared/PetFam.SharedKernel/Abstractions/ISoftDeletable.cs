namespace PetFam.Shared.SharedKernel.Abstractions
{
    public interface ISoftDeletable
    {
        void Delete();
        void Restore();
    }
}
