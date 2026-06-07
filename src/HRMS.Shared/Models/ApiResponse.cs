namespace HRMS.Shared.Models;
public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    public string? TraceId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public static ApiResponse<T> Ok(T data, string? message = null) => new() { Success = true, Data = data, Message = message };
    public static ApiResponse<T> Fail(string error) => new() { Success = false, Errors = new[] { error } };
    public static ApiResponse<T> Fail(IEnumerable<string> errors) => new() { Success = false, Errors = errors };
}
public class ApiResponse : ApiResponse<object>
{
    public static ApiResponse Ok(string? message = null) => new() { Success = true, Message = message };
    public new static ApiResponse Fail(string error) => new() { Success = false, Errors = new[] { error } };
}
