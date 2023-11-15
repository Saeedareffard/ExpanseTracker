using System.Net;

namespace Infrastructure.Exceptions;

public class CustomException : Exception
{
    public CustomException(string message, List<string> errors = default,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public List<string>? ErrorMessages { get; }
    public HttpStatusCode StatusCode { get; }
}