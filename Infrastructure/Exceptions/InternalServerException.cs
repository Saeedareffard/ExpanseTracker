using System.Net;
using Infrastructure.Exceptions;

namespace Infrastructure.Exceptions;

public class InternalServerException : CustomException
{
    public InternalServerException(string message, List<string>? errors = default)
        : base(message, errors, HttpStatusCode.InternalServerError)
    {
    }
}