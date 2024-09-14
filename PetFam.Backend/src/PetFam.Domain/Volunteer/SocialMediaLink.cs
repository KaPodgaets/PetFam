using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer
{
    public record SocialMediaLink
    {

        private SocialMediaLink(string name, string link)
        {
            Name = name;
            Link = link;
        }
        public string Name { get; }
        public string Link { get; }

        public static Result<SocialMediaLink> Create(string name, string link)
        {
            if (string.IsNullOrEmpty(name))
                return Errors.General.ValueIsInvalid(nameof(Name));
            if (string.IsNullOrEmpty(link))
                return Errors.General.ValueIsInvalid(nameof(Link));

            return new SocialMediaLink(name, link);
        }
    }
}
