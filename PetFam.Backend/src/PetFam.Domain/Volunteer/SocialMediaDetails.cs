﻿using PetFam.Domain.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetFam.Domain.Volunteer
{
    public record SocialMediaDetails
    {
        //ef core
        private SocialMediaDetails()
        {

        }

        private SocialMediaDetails(IEnumerable<SocialMediaLink> value)
        {
            Value = value.ToList();
        }

        public IReadOnlyList<SocialMediaLink> Value { get; } = null!;

        public static Result<SocialMediaDetails> Create(IEnumerable<SocialMediaLink> value)
        {
            if (value.ToList().Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return Errors.General.ValueIsRequired(nameof(SocialMediaDetails));

            return new SocialMediaDetails(value);
        }
    }
}
