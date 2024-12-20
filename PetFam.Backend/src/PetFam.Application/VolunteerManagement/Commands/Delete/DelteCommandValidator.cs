﻿using FluentValidation;

namespace PetFam.Application.VolunteerManagement.Commands.Delete
{
    public class DelteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DelteCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
