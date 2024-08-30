namespace PetFam.Domain.Models
{
    public class Requisite
    {
        public Requisite(string name, string accountNumber, string paymentInstrution)
        {
            SetName(name);
            SetAccountNumber(accountNumber);
            SetPaymentInstruction(paymentInstrution);
        }

        public string Name { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string PaymentInstruction { get; set; } = string.Empty;

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty or null.");
            }

            Name = name;
        }

        public void SetAccountNumber(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                throw new ArgumentException("Account Number cannot be empty or null.");
            }

            AccountNumber = accountNumber;
        }

        public void SetPaymentInstruction(string paymentInstruction)
        {
            if (string.IsNullOrWhiteSpace(paymentInstruction))
            {
                throw new ArgumentException("Payment Instruction cannot be empty or null.");
            }

            PaymentInstruction = paymentInstruction;
        }
    }
}
