﻿using System.Net;
using System.Runtime.Serialization;

namespace Business.Commons.Exceptions;

[Serializable]
public sealed class BadRequestException : Exception
{
    public BadRequestException()
    {

    }

    public BadRequestException(string message)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        Message = message;
    }

    public BadRequestException(HttpStatusCode httpStatusCode, string message)
    {
        StatusCode = (int)httpStatusCode;
        Message = message;
    }

    public BadRequestException(HttpStatusCode httpStatusCode, string message, object errors)
    {
        StatusCode = (int)httpStatusCode;
        Message = message;
        Errors = errors;
    }


    public int StatusCode { get; set; }

    public bool Succeeded { get; set; } = false;

    public new string Message { get; set; }

    public object Errors { get; }

    public new object Data { get; set; } = null;

    private BadRequestException(SerializationInfo serializationInfo, StreamingContext streamingContext)
    {
        throw new NotImplementedException();
    }
}