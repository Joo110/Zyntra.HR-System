using System.Net;
using System.Text.Json;
using HRMS.Application.Common.Exceptions;
using HRMS.Shared.Models;
using ValidationException = HRMS.Application.Common.Exceptions.ValidationException;

namespace HRMS.WebApi.Middlewares;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    { _next = next; _logger = logger; }

    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception ex) { await HandleExceptionAsync(context, ex); }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        context.Response.ContentType = "application/json";
        var response = new ApiResponse<object>();
        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                response = new ApiResponse<object> { Success = false, Message = "Validation failed.", Errors = validationEx.Errors.SelectMany(e => e.Value) };
                break;
            case NotFoundException notFoundEx:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                response = ApiResponse<object>.Fail(notFoundEx.Message);
                break;
            case ForbiddenException forbidEx:
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                response = ApiResponse<object>.Fail(forbidEx.Message);
                break;
            case HRMS.Domain.Exceptions.UnauthorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                response = ApiResponse<object>.Fail("Unauthorized.");
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                response = ApiResponse<object>.Fail("An internal server error occurred.");
                break;
        }
        response.TraceId = context.TraceIdentifier;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }));
    }
}
