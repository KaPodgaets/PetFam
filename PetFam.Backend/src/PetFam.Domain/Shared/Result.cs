namespace PetFam.Domain.Shared
{
    public class Result
    {
        private readonly Error? _error;
        protected Result(bool isSuccess, Error? error)
        {
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException();

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException();

            _error = error;
            IsSuccess = isSuccess;
        }

        public Error Error
            => IsFailure
            ? _error ?? throw new InvalidOperationException("Error is null but operation was failed")
            : throw new InvalidOperationException("The error of a successful result cannot be accessed");
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);
        public static implicit operator Result(Error error) => new(false, error);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        public Result(TValue? value, bool isSuccess, Error? error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value ?? throw new InvalidOperationException("Value is null but operation was successful")
            : throw new InvalidOperationException("The value of a failure result cannot be accessed");

        public static Result<TValue> Success(TValue value)
            => new(value, true, Error.None);

        public new static Result<TValue> Failure(Error error)
            => new(default!, false, error);

        public static implicit operator Result<TValue>(TValue value)
            => new(value, true, Error.None);

        public static implicit operator Result<TValue>(Error error)
            => new(default!, false, error);
    }
}
