﻿using PetFam.Application.SpeciesManagement.Commands.CreateBreed;

namespace PetFam.Api.Requests.Species
{
    public record CreateBreedRequest(Guid SpeciesId, string Name)
    {
        public CreateBreedCommand ToCommand()
        {
            return new CreateBreedCommand(SpeciesId, Name);
        }
    };
}
