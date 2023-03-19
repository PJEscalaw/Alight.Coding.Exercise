namespace Business.Commons.Exceptions;

[Serializable]
public sealed class NotFoundException : Exception
{
    public NotFoundException(string message)
    {
        Message = message;        
    }
    public NotFoundException(int statusCode, bool succeded, string message, object errors)
    {
        StatusCode = statusCode;
        Succeeded = succeded;
        Message = message;
        Errors = errors;
    }

    public int StatusCode { get; set; } = 404;
    public bool Succeeded { get; set; } 
    public string Message { get; set; }
    public object Errors { get; set; }
}
