
namespace Auth.Api.Common;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await SendExceptionResponse(exception, context);
        }
    }

    private static async Task SendExceptionResponse(Exception exception, HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var errorResponse = new
        {
            message = "Unexpected error happened.",
            detail = exception.Message
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    }

}