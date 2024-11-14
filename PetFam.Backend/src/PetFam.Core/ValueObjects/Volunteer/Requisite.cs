namespace PetFam.Shared.ValueObjects.Volunteer
{
    public record Requisite
    {
        private Requisite(string name, string accountNumber, string paymentInstruction)
        {
            Name = name;
            AccountNumber = accountNumber;
            PaymentInstruction = paymentInstruction;
        }

        public string Name { get; }
        public string AccountNumber { get; }
        public string PaymentInstruction { get; }

        public static Result<Requisite> Create(string name,
            string accountNumber,
            string paymentInstruction)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid(nameof(Name)).ToErrorList();
            if (string.IsNullOrWhiteSpace(accountNumber))
                return Errors.General.ValueIsInvalid(nameof(AccountNumber)).ToErrorList();
            if (string.IsNullOrWhiteSpace(paymentInstruction))
                return Errors.General.ValueIsInvalid(nameof(PaymentInstruction)).ToErrorList();

            return new Requisite(name, accountNumber, paymentInstruction);
        }
    }
}
