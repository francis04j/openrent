using System;

namespace OpenRentStringLib
{
    public readonly record struct Error(string Code, string Message)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public bool IsNone => string.IsNullOrEmpty(Code);
    }

    public readonly struct Result<T>
    {
        private readonly T? _value;

        private Result(T value)
        {
            IsSuccess = true;
            _value = value;
            Error = Error.None;
        }

        private Result(Error error)
        {
            if (error.IsNone)
            {
                throw new ArgumentException("A failure result must define an error.", nameof(error));
            }

            IsSuccess = false;
            _value = default;
            Error = error;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Cannot access the value of a failed result.");

        public Error Error { get; }

        public static Result<T> Success(T value)
        {
            return new Result<T>(value);
        }

        public static Result<T> Failure(Error error)
        {
            return new Result<T>(error);
        }
    }
}