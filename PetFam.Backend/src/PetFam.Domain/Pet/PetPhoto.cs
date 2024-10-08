﻿using PetFam.Domain.Shared;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetFam.Domain.Pet
{
    public record PetPhoto
    {
        private PetPhoto(string filePath, bool isMain)
        {
            FilePath = filePath;
            IsMain = isMain;
        }
        public string FilePath { get; }
        public bool IsMain { get; }

        public static Result<PetPhoto> Create(string filePath, bool isMain)
        {
            if (string.IsNullOrEmpty(filePath))
                return Errors.General.ValueIsInvalid(nameof(FilePath));

            return new PetPhoto(filePath, isMain);
        }
    }
}
