using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public record AccountInfo
    {
        private AccountInfo(string number, string bankName)
        {
            Number = number;
            BankName = bankName;
        }

        public string Number { get; }
        public string BankName { get; }

        public static Result<AccountInfo> Create(string number,
            string bankName)
        {
            if (string.IsNullOrWhiteSpace(number))
                return "Account number cannot be empty.";

            if (string.IsNullOrWhiteSpace(bankName))
                return "Bank's name cannot be empty.";

            return new AccountInfo(number, bankName);
        }
    }
}
