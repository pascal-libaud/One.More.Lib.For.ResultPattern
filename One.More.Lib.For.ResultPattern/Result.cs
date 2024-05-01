using System.Diagnostics.CodeAnalysis;

namespace One.More.Lib.For.ResultPattern;

public record Result
{
    public virtual bool IsSuccess { get; private init; }
    public Error[] Errors { get; private init; } = [];

    public static Result Success { get; } = new Result { IsSuccess = true };

    public static Result<T> Create<T>(T result)
    {
        return new Result<T> { IsSuccess = true, Value = result };
    }

    public static Result<T> Fail<T>(params Error[] errors)
    {
        return new Result<T> { IsSuccess = false, Errors = errors, Value = default! };
    }
}

public record Result<T> : Result
{
    [MemberNotNullWhen(true, nameof(Value))]
    public override bool IsSuccess => base.IsSuccess;

    public required T? Value { get; init; }

    public static implicit operator Result<T>(Error[] errors)
    {
        return Fail<T>(errors.ToArray());
    }

    public static implicit operator Result<T>(Error error)
    {
        return Fail<T>(error);
    }

    public static implicit operator Result<T>(T value)
    {
        return Create(value);
    }

    public static implicit operator T(Result<T> result)
    {
        if (!result.IsSuccess)
            throw new Exception();

        return result.Value;
    }

    public bool TryIsSuccess([NotNullWhen(true)] out T? value)
    {
        value = Value;
        return IsSuccess;
    }
}