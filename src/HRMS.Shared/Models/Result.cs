namespace HRMS.Shared.Models;
public class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;
    public string? ErrorMessage { get; protected set; }
    public IEnumerable<string> Errors { get; protected set; } = Enumerable.Empty<string>();
    protected Result(bool isSuccess, string? errorMessage, IEnumerable<string>? errors = null)
    { IsSuccess = isSuccess; ErrorMessage = errorMessage; Errors = errors ?? Enumerable.Empty<string>(); }
    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error, new[] { error });
    public static Result Failure(IEnumerable<string> errors) => new(false, "One or more errors occurred.", errors);
}
public class Result<T> : Result
{
    public T? Value { get; private set; }
    protected Result(bool isSuccess, T? value, string? errorMessage, IEnumerable<string>? errors = null)
        : base(isSuccess, errorMessage, errors) { Value = value; }
    public static Result<T> Success(T value) => new(true, value, null);
    public new static Result<T> Failure(string error) => new(false, default, error, new[] { error });
    public new static Result<T> Failure(IEnumerable<string> errors) => new(false, default, "One or more errors occurred.", errors);
}
