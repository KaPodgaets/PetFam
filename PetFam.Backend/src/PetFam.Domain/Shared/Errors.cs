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

            public static Error ValueIsNotUnique(string? name = null)
            {
                var label = name ?? "value";
                return Error.Conflict("value.not.unique", $"{label} is not unique");
            }

            public static Error NotFound(Guid? id = null)
            {
                var forId = id == null ? "" : $" for Id {id}";
                return Error.Validation("record.not.found", $"record not found {forId}");
            }

            public static Error NotFound(string? name = null)
            {
                var forName = name == null ? "" : $" for Id {name}";
                return Error.Validation("record.not.found", $"record not found {forName}");
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
