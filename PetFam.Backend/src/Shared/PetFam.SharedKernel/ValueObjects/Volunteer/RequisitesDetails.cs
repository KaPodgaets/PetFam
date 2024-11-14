namespace PetFam.Shared.SharedKernel.ValueObjects.Volunteer
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

        public IReadOnlyList<Requisite> Value { get; } = null!;

        public static Result<RequisitesDetails> Create(IEnumerable<Requisite> value)
        {
            if (value.ToList().Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return Errors.General.ValueIsRequired(nameof(RequisitesDetails)).ToErrorList();

            return new RequisitesDetails(value);
        }
    }
}
