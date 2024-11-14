﻿using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Errors;

namespace PetFam.Shared.Models
{
    public record Envelope
    {
        public object? Result { get; }
        public ErrorList? Errors { get; }
        public DateTime TimeGenerated { get; }

        private Envelope(object? result, ErrorList? errors)
        {
            Result = result;
            Errors = errors;
            TimeGenerated = DateTime.Now;
        } 

        public static Envelope Ok(object? result = null) =>
            new(result, null);

        public static Envelope Error(ErrorList errors) =>
            new(null, errors);
    }
}