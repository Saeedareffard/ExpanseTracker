using System.Net;
using FluentValidation;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Infrastructure.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ISerializerService _serializer;

    public ExceptionMiddleware(ISerializerService serializer)
    {
        _serializer = serializer;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            e = GetInnermostException(e);
            var errorResult = new ErrorResult
            {
                Source = e.TargetSite?.DeclaringType.FullName,
                Exception = e.Message.Trim()
            };

            if (e is ValidationException fluentException)
            {
                errorResult.Messages.AddRange(fluentException.Errors.Select(error => error.ErrorMessage));
                errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                errorResult.StatusCode = e switch
                {
                    CustomException customException => (int)customException.StatusCode,
                    KeyNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };
            }

            Log.Error(e, $"Request failed with Status Code {errorResult.StatusCode}");

            await WriteResponseAsync(context, errorResult);
        }
    }

    private Exception GetInnermostException(Exception exception)
    {
        while (exception.InnerException != null)
            exception = exception.InnerException;

        return exception;
    }

    private async Task WriteResponseAsync(HttpContext context, ErrorResult errorResult)
    {
        var response = context.Response;
        if (!response.HasStarted)
        {
            response.ContentType = "application/json";
            response.StatusCode = errorResult.StatusCode;
            await response.WriteAsync(_serializer.Serialize(errorResult));
        }
        else
        {
            Log.Warning("Response has already started. Can't write the error message.");
        }
    }
}
