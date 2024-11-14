namespace PetFam.Species.Contracts.SpeciesRequests
{
    public record CreateBreedRequest(Guid SpeciesId, string Name)
    {
        public CreateBreedCommand ToCommand()
        {
            return new CreateBreedCommand(SpeciesId, Name);
        }
    };
}
