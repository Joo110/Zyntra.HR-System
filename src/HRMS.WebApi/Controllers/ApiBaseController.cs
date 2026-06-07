using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using HRMS.Shared.Models;

namespace HRMS.WebApi.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class ApiBaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess && result.Value != null) return Ok(ApiResponse<T>.Ok(result.Value));
        if (result.IsSuccess) return NoContent();
        return BadRequest(ApiResponse<T>.Fail(result.Errors));
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess) return Ok(ApiResponse.Ok());
        return BadRequest(ApiResponse.Fail(result.Errors));
    }

    protected IActionResult HandleCreatedResult<T>(Result<T> result, string? routeName = null, object? routeValues = null)
    {
        if (result.IsSuccess && result.Value != null)
        {
            if (routeName != null) return CreatedAtRoute(routeName, routeValues, ApiResponse<T>.Ok(result.Value));
            return StatusCode(201, ApiResponse<T>.Ok(result.Value));
        }
        return BadRequest(ApiResponse<T>.Fail(result.Errors));
    }
}
