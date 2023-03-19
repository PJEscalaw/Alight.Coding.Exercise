using System.Net;
using System.Runtime.Serialization;

namespace Business.Commons.Exceptions;

[Serializable]
public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {

    }

    public NotFoundException(string name, string fieldName, object key)
    {
        throw new ErrorException(HttpStatusCode.NotFound, $"{fieldName} > ({key}) was not found in [{name}] entity.");
    }

    private NotFoundException(SerializationInfo info, StreamingContext context)
    : base(info, context)
    {
    }

    public NotFoundException(string? message) : base(message)
    {
    }

    public NotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
