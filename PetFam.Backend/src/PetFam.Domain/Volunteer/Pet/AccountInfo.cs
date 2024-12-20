﻿using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer.Pet
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
                return Errors.General.ValueIsInvalid(nameof(Number)).ToErrorList(); ;

            if (string.IsNullOrWhiteSpace(bankName))
                return Errors.General.ValueIsInvalid(nameof(BankName)).ToErrorList(); ;

            return new AccountInfo(number, bankName);
        }
    }
}
