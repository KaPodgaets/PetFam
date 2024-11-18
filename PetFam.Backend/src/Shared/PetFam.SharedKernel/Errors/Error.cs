namespace PetFam.Shared.SharedKernel.Errors
{
    public record Error
    {
        private const string SEPARATOR = "||";
        private Error(string code, string message, ErrorType type, string? invalidField = null)
        {
            Code = code;
            Message = message;
            Type = type;
            InvalidField = invalidField;
        }

        public string Code { get; } = string.Empty;
        public string Message { get; } = string.Empty;
        public ErrorType Type { get; }
        public string? InvalidField { get; } = null;

        public static readonly Error None =
            new(string.Empty, string.Empty, ErrorType.None, null);

        public static Error Validation(string code, string message, string? invalidField = null)
        {
            return new Error(code, message, ErrorType.Validation, invalidField);
        }

        public static Error NotFound(string code, string message)
        {
            return new Error(code, message, ErrorType.NotFound);
        }

        public static Error Failure(string code, string message)
        {
            return new Error(code, message, ErrorType.Failure);
        }
        public static Error Conflict(string code, string message)
        {
            return new Error(code, message, ErrorType.Conflict);
        }

        public string Serialize()
        {
            return string.Join(SEPARATOR, Code, Message, Type);
        }

        public ErrorList ToErrorList()
            => new([this]);
    }
    public enum ErrorType
    {
        None,
        Validation,
        NotFound,
        Failure,
        Conflict
    }
}
