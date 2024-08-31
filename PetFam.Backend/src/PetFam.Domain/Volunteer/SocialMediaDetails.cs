using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer
{
    public record SocialMediaDetails
    {
        //ef core
        private SocialMediaDetails()
        {

        }

        private SocialMediaDetails(List<SocialMediaLink> value)
        {
            Value = value;
        }

        public IReadOnlyList<SocialMediaLink> Value { get; }

        public static Result<SocialMediaDetails> Create(List<SocialMediaLink> value)
        {
            if (value.Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return $"In social media should be at least {Constants.MIN_ELEMENTS_IN_ARRAY} link";

            return new SocialMediaDetails(value);
        }
    }
}
