﻿using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Commands.CreateBreed
{
    public class CreateBreedCommandValidator: AbstractValidator<CreateBreedCommand>
    {
        public CreateBreedCommandValidator()
        {
            RuleFor(x => x.SpeciesId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
