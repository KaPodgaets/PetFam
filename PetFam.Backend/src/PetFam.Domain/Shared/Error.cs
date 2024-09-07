namespace PetFam.Domain.Shared
{
    public record Error
    {
        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public string Code { get; } = string.Empty;
        public string Message { get; } = string.Empty;
        public ErrorType Type { get; }

        public static Error Validation(string code, string message)
        {
            return new Error(code, message, ErrorType.Validation);
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
