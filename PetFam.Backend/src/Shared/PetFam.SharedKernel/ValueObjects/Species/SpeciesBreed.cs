﻿using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Shared.SharedKernel.ValueObjects.Species
{
    public record SpeciesBreed
    {
        public SpeciesId SpeciesId { get; }
        public Guid BreedId { get; }
        private SpeciesBreed(SpeciesId speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<SpeciesBreed> Create(SpeciesId speciesId, Guid breedId)
        {
            if (speciesId.Value == Guid.Empty)
                return Errors.Errors.General.ValueIsInvalid(nameof(SpeciesId)).ToErrorList();

            if (breedId == Guid.Empty)
                return Errors.Errors.General.ValueIsInvalid(nameof(BreedId)).ToErrorList();

            return new SpeciesBreed(speciesId, breedId);
        }
    }
}
