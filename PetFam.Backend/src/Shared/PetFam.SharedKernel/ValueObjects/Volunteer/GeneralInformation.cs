namespace PetFam.Shared.SharedKernel.ValueObjects.Volunteer
{
    public record GeneralInformation
    {
        private GeneralInformation(string bioEducation, string shortDescription)
        {
            BioEducation = bioEducation;
            ShortDescription = shortDescription;
        }

        public string BioEducation { get; }
        public string ShortDescription { get; }

        public static Result<GeneralInformation> Create(string bioEducation, string shortDescription)
        {
            if (bioEducation.Length <= Constants.MAX_LONG_TEXT_LENGTH)
            {
                Errors.General.ValueIsInvalid("Education");
            }

            if (shortDescription.Length <= Constants.MAX_LONG_TEXT_LENGTH)
            {
                Errors.General.ValueIsInvalid("Description");
            }

            return new GeneralInformation(bioEducation, shortDescription);
        }
    }
}
