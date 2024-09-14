namespace PetFam.Domain.Shared
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsInvalid(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is invalid.");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id {id}";
                return Error.Validation("record.not.found", $"record not found {forId}");
            }

            public static Error NotFound(string? email = null)
            {
                var forEmail = email == null ? "" : $" for Id {email}";
                return Error.Validation("record.not.found", $"record not found {forEmail}");
            }

            public static Error ValueIsRequired(string? name = null)
            {
                var label = name ?? "value";
                return Error.Validation("value.is.invalid", $"{label} is required.");
            }

            public static Error Failure()
            {
                return Error.Failure("internal.failure", "unknown server error");
            }
        }
        public static class Volunteer
        {
            public static Error AlreadyExist(string? email = null)
            {
                return Error.Validation("volunteer.already.exist", $"volunteer with email: {email} already exist");
            }
        }
    }
}
