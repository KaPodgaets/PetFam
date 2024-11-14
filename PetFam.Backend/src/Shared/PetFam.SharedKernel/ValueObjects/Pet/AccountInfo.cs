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

        public static Result.Result<AccountInfo> Create(string number,
            string bankName)
        {
            if (string.IsNullOrWhiteSpace(number))
                return Errors.Errors.General.ValueIsInvalid(nameof(Number)).ToErrorList();

            if (string.IsNullOrWhiteSpace(bankName))
                return Errors.Errors.General.ValueIsInvalid(nameof(BankName)).ToErrorList();

            return new AccountInfo(number, bankName);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
            yield return BankName;
        }
    }
}
