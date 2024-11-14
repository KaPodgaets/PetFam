﻿using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Shared.SharedKernel.ValueObjects.Pet
{
    public record PetPhoto
    {
        private PetPhoto(string filePath, bool isMain = false)
        {
            FilePath = filePath;
            IsMain = isMain;
        }

        public bool IsMain { get; }
        public string FilePath { get; }

        public static Result<PetPhoto> Create(string filePath, bool isMain = false)
        {
            if (string.IsNullOrEmpty(filePath))
                return Errors.Errors.General.ValueIsInvalid(nameof(FilePath)).ToErrorList();

            return new PetPhoto(filePath, isMain);
        }
    }
}