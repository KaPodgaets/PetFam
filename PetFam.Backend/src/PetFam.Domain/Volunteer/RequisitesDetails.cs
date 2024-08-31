using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer
{
    public record RequisitesDetails
    {
        private RequisitesDetails()
        {
        }
        private RequisitesDetails(List<Requisite> value)
        {
            Value = value;
        }

        public IReadOnlyList<Requisite> Value { get; }

        public static Result<RequisitesDetails> Create(List<Requisite> value)
        {
            if (value.Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return $"In requisites should be at least {Constants.MIN_ELEMENTS_IN_ARRAY} requisite";

            return new RequisitesDetails(value);
        }
    }
}
