using System;

namespace StoreBackend.Exceptions;

public class BadRequestResponseException : MessageException
{
    public BadRequestResponseException(string message) : base(message)
    {
    }

    public BadRequestResponseException(string message, Exception innerException) : base(message, innerException)
        {
        }
}
