using Business.Commons.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static string ReturnBadRequestResponse(ErrorException error)
        => JsonSerializer.Serialize(new
        {
            error.StatusCode,
            error.Succeeded,
            error.Message,
        });

    private static string ReturnInternalServerResponse(Exception error)
    {
        if (error is null)
            throw new ArgumentNullException(nameof(error));

        var logError = JsonSerializer.Serialize(new
        {
            ErrorMessage = error.Message,
            error.StackTrace,
            error.Source,
            InnerException = error.InnerException == null ? "" : error.InnerException.Message,
            DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
        });

        Log.Error(logError, "[SERVER ERROR]");

        return JsonSerializer.Serialize(new
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Message = "The API has encountered an Internal Server Error. Please try again.",
            ErrorDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff")
        });
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        switch (ex)
        {
            case ErrorException e:
                context.Response.StatusCode = e.StatusCode;
                await context.Response.WriteAsync(ReturnBadRequestResponse(e));
                break;
            case Exception e:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ReturnInternalServerResponse(e));
                break;
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ReturnInternalServerResponse(ex));
                break;
        }
    }
}