namespace PetFam.Domain.Shared
{
    public class Result
    {
        private readonly string? _errorMessage;
        protected Result(bool isSuccess, string? errorMessage)
        {
            _errorMessage = errorMessage;
            IsSuccess = isSuccess;
        }

        public string ErrorMessage
            => IsFailure
            ? _errorMessage ?? throw new InvalidOperationException("Error message is null but operation was failed")
            : throw new InvalidOperationException("The error message of a successful result cannot be accessed");
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public static Result Success() => new(true, null);
        public static Result Failure(string errorMessage) => new(false, errorMessage);
        public static implicit operator Result(string errorMessage) => new(false, errorMessage);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool isSuccess, string? errorMessage)
            : base(isSuccess, errorMessage)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value ?? throw new InvalidOperationException("Value is null but operation was successful")
            : throw new InvalidOperationException("The value of a failure result cannot be accessed");


        public static Result<TValue> Success(TValue value) => new(value, true, null);
        public new static Result<TValue> Failure(string errorMessage) => new(default!, false, errorMessage);
        public static implicit operator Result<TValue>(TValue value) => new(value, true, null);
        public static implicit operator Result<TValue>(string errorMessage) => new(default!, true, errorMessage);
    }
}
