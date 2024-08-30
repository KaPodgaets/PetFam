namespace PetFam.Domain.Models
{
    public class SocialMediaLink
    {
        public SocialMediaLink(string name, string link)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("Name of social media can not be empty");
            if (string.IsNullOrEmpty(link)) throw new ArgumentNullException("Link to social media can not be empty");

            Name = name;
            Link = link;
        }
        public string Name { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
    }
}
