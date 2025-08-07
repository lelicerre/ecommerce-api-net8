using System.Net;
using System.Text.Json;
using EcommerceApi.Models;

namespace EcommerceApi.Middleware;

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
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var error = new ErrorDetails
        {
            Timestamp = DateTime.UtcNow,
            Message = "Erro interno no servidor.",
            Details = exception.Message
        };

        return context.Response.WriteAsync(JsonSerializer.Serialize(error));
    }
}