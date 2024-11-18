using PetFam.Shared.SharedKernel.Errors;

namespace PetFam.Shared.SharedKernel.Result
{
    public class Result
    {
        private readonly ErrorList? _errors;
        protected Result(bool isSuccess, ErrorList? errors)
        {
            if (isSuccess && errors != null)
                throw new InvalidOperationException();

            if (!isSuccess && errors == null)
                throw new InvalidOperationException();

            _errors = errors;
            IsSuccess = isSuccess;
        }

        public ErrorList Errors
            => IsFailure
            ? _errors ?? throw new InvalidOperationException("Error is null but operation was failed")
            : throw new InvalidOperationException("The error of a successful result cannot be accessed");
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public static Result Success() => new(true, null);
        public static Result Failure(ErrorList errors) => new(false, errors);
        public static implicit operator Result(ErrorList errors) => new(false, errors);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue? _value;

        private Result(TValue? value, bool isSuccess, ErrorList? errors)
            : base(isSuccess, errors)
        {
            _value = value;
        }

        public TValue Value => IsSuccess
            ? _value ?? throw new InvalidOperationException("Value is null but operation was successful")
            : throw new InvalidOperationException("The value of a failure result cannot be accessed");

        public static Result<TValue> Success(TValue value)
            => new(value, true, null);

        public new static Result<TValue> Failure(ErrorList errors)
            => new(default!, false, errors);

        public static implicit operator Result<TValue>(TValue value)
            => new(value, true, null);

        public static implicit operator Result<TValue>(ErrorList errors)
            => new(default!, false, errors);
    }
}
