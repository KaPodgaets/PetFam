﻿using PetFam.Application.FileProvider;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.AddPetPhotos
{
    public record PetAddPhotosCommand(
        Guid VolunteerId,
        Guid PetId,
        Content Content):ICommand;
}
