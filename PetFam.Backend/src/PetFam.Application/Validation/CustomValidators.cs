﻿using FluentValidation;
using FluentValidation.Results;
using PetFam.Domain.Shared;

namespace PetFam.Application.Validation
{
    public static class CustomValidators
    {
        public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>
            (this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject>> factoryMethod)
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject> result = factoryMethod(value);

                if (result.IsSuccess)
                    return;

                var failure = new ValidationFailure(context.PropertyName, result.Error.Message)
                {
                    ErrorCode = result.Error.Code // Setting the error code
                };

                context.AddFailure(failure);
            });
        }
    }
}
