namespace PetFam.Domain.Shared
{
    public class Result
    {
        protected Result(bool isSuccess, string? errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccess = isSuccess;
        }

        public string? ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public static Result Success() => new(true, null);
        public static Result Failure(string errorMessage) => new(false, errorMessage);
        public static implicit operator Result(string errorMessage) => new(false, errorMessage);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public Result(TValue value, bool isSuccess, string? errorMessage)
            : base(isSuccess, errorMessage)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value
            : throw new InvalidOperationException("The value of a failure result can not be accessed");

        public static Result<TValue> Success(TValue value) => new(value, true, null);
        public new static Result<TValue> Failure(string errorMessage) => new(default!, false, errorMessage);
        public static implicit operator Result<TValue>(TValue value) => new(value, true, null);
        public static implicit operator Result<TValue>(string errorMessage) => new(default!, true, errorMessage);
    }
}
