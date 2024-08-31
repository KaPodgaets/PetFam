using PetFam.Domain.Pet;
using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer
{
    public record RequisitesDetails
    {
        private RequisitesDetails()
        {
        }
        private RequisitesDetails(IEnumerable<Requisite> value)
        {
            Value = value.ToList();
        }

        public IReadOnlyList<Requisite> Value { get; }

        public static Result<RequisitesDetails> Create(IEnumerable<Requisite> value)
        {
            if (value.ToList().Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return $"In requisites should be at least {Constants.MIN_ELEMENTS_IN_ARRAY} requisite";

            return new RequisitesDetails(value);
        }
    }
}
