namespace HRMS.WebApi.Middlewares;
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger) { _next = next; _logger = logger; }
    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("HTTP {Method} {Path} started", context.Request.Method, context.Request.Path);
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await _next(context);
        sw.Stop();
        _logger.LogInformation("HTTP {Method} {Path} completed in {Elapsed}ms with status {StatusCode}", context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds, context.Response.StatusCode);
    }
}
