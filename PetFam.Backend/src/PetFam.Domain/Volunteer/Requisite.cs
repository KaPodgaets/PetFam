using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer
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
                return "Name cannot be empty or null.";
            if (string.IsNullOrWhiteSpace(accountNumber))
                return "Account Number cannot be empty or null.";
            if (string.IsNullOrWhiteSpace(paymentInstruction))
                return "Payment Instruction cannot be empty or null.";

            return new Requisite(name, accountNumber, paymentInstruction);
        }
    }
}
