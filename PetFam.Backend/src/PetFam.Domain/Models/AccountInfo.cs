namespace PetFam.Domain.Models
{
    public class AccountInfo
    {
        public AccountInfo(string number, string bankName)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Account number cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(bankName))
            {
                throw new ArgumentException("Bank's name cannot be empty.");
            }

            Number = number;
            BankName = bankName;
        }

        public string Number { get; private set; } = string.Empty;
        public string BankName { get; private set; } = string.Empty;
    }
}
