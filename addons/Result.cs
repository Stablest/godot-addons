using System;

namespace Addons.addons;

public class Result<T>
{
    private readonly Action<T>? _onSuccess;
    private readonly Action<T>? _onFailure;
    private readonly Action<T>? _onCompleted;

    private Result(Action<T>? success, Action<T>? failure, Action<T>? completed)
    {
        _onSuccess = success;
        _onFailure = failure;
        _onCompleted = completed;
    }

    public void CompleteWithSuccess(T value)
    {
        _onSuccess?.Invoke(value);
        _onCompleted?.Invoke(value);
    }

    public void CompleteWithFailure(T value)
    {
        _onFailure?.Invoke(value);
        _onCompleted?.Invoke(value);
    }

    static public Result<T> CreateResult(Action<T>? onSuccess = null, Action<T>? onFailure = null, Action<T>? onCompleted = null)
    {
        return new Result<T>(onSuccess, onFailure, onCompleted);
    }
    
    public static Result<T> Success(Action<T> onSuccess)
        => new(onSuccess, null, null);

    public static Result<T> Failure(Action<T> onFailure)
        => new(null, onFailure, null);
}