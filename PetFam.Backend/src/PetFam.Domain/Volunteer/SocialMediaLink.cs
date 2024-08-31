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
                return "Name of social media can not be empty";
            if (string.IsNullOrEmpty(link))
                return "Link to social media can not be empty";

            return new SocialMediaLink(name, link);
        }
    }
}
