﻿using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Shared.SharedKernel.ValueObjects.Volunteer
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
                return Errors.Errors.General.ValueIsInvalid(nameof(Name)).ToErrorList();
            if (string.IsNullOrEmpty(link))
                return Errors.Errors.General.ValueIsInvalid(nameof(Link)).ToErrorList();

            return new SocialMediaLink(name, link);
        }
    }
}