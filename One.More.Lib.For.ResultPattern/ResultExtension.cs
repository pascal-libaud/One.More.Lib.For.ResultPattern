using One.More.Lib.For.Linq;

namespace One.More.Lib.For.ResultPattern;

public static class ResultExtension
{
    public static Result<TResult> Map<TSource, TResult>(this Result<TSource> result, Func<TSource?, TResult> selector) => Do(result, selector);

    public static Result<TResult> Do<TSource, TResult>(this Result<TSource> result, Func<TSource, TResult> selector)
    {
        if (!result.TryIsSuccess(out var value))
            return result.Errors;
        else
            return selector(value);
    }

    public static Result<T> Tap<T>(this Result<T> result, Action<T?> action) => Do(result, action);

    public static Result<T> Do<T>(this Result<T> result, Action<T?> action)
    {
        if (!result.IsSuccess)
            return result.Errors;
        else
        {
            action(result.Value);
            return result;
        }
    }

    public static Result<IEnumerable<U>> Select<T, U>(this Result<IEnumerable<T>> result, Func<T, U> selector)
    {
        return result.Do(x => x.Select(selector));
    }

    public static Result<IEnumerable<T>> Where<T>(this Result<IEnumerable<T>> result, Func<T, bool> predicate)
    {
        return result.Do(x => x.Where(predicate));
    }

    public static Result<IAsyncEnumerable<U>> SelectAsync<T, U>(this Result<IAsyncEnumerable<T>> result, Func<T, U> selector)
    {
        return result.Do(x => x.OmSelectAsync(selector));
    }

    public static Result<IAsyncEnumerable<U>> SelectAsync<T, U>(this Result<IAsyncEnumerable<T>> result, Func<T, Task<U>> selector)
    {
        return result.Do(x => x.OmSelectAsync(selector));
    }
}