﻿using PetFam.Shared.Abstractions;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.Create
{
    public record CreateSpeciesCommand(string Name):ICommand;
}