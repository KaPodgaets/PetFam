﻿using PetFam.Shared.Abstractions;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.Delete
{
    public record DeleteSpeciesCommand(Guid Id):ICommand;
}