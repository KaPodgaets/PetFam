namespace PetFam.Domain.Models
{
    public class AccountInfo
    {
        public AccountInfo(string number, string bankName)
        {
            if (string.IsNullOrWhiteSpace(number) ||
                string.IsNullOrWhiteSpace(bankName))
            {
                throw new ArgumentException("First name and last name cannot be empty.");
            }

            Number = number;
            BankName = bankName;
        }

        public string Number { get; set; } = string.Empty;
        public string BankName { get; set; } = string.Empty;
    }
}
