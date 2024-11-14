using CSharpFunctionalExtensions;

namespace PetFam.Shared.SharedKernel.ValueObjects.Pet
{
    public class AccountInfo :ValueObject
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
                return Errors.General.ValueIsInvalid(nameof(Number)).ToErrorList();

            if (string.IsNullOrWhiteSpace(bankName))
                return Errors.General.ValueIsInvalid(nameof(BankName)).ToErrorList();

            return new AccountInfo(number, bankName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
