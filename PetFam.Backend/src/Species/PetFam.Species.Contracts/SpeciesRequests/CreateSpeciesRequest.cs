namespace PetFam.Species.Contracts.SpeciesRequests
{
    public record CreateSpeciesRequest(string Name)
    {
        public CreateSpeciesCommand ToCommand()
        {
            return new CreateSpeciesCommand(Name);
        }
    }
}
