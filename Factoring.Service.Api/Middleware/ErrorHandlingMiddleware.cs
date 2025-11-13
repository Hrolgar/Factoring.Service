using System.Net;
using System.Text.Json;
using Factoring.Service.Application.Exceptions;

namespace Factoring.Service.Api.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, logger);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception,
        ILogger<ErrorHandlingMiddleware> logger)
    {
        var status = HttpStatusCode.InternalServerError;
        var message = "An unexpected error occurred.";

        switch (exception)
        {
            case NotFoundException:
                status = HttpStatusCode.NotFound;
                message = exception.Message;
                break;
            case ValidationException:
                status = HttpStatusCode.BadRequest;
                message = exception.Message;
                break;
        }

        if (status == HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception, "An error occurred: {Message}", exception.Message);
        }

        var result = JsonSerializer.Serialize(new { error = message });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        return context.Response.WriteAsync(result);
    }
}