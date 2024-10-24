﻿using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateRequisites
{
    public class UpdateRequisitesValidator : AbstractValidator<UpdateRequisitesRequest>
    {
        public UpdateRequisitesValidator()
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleForEach(v => v.Dto.Requisites)
                .MustBeValueObject(x => Requisite.Create(x.Name, x.AccountNumber, x.PaymentInstruction));
        }
    }
}
