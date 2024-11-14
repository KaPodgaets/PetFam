﻿using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Email,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks,
        IEnumerable<RequisiteDto>? Requisites) :ICommand;
}