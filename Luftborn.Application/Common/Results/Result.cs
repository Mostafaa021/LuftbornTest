namespace Luftborn.Application.Common.Results;

public class Result<T> 
{
    public bool Succeeded { get; set; }
    public string? Message { get; set; }
    public string? ErrorCode { get; set; }
    public T? Data { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public static Result<T> Success(T data, string? message = null)
        => new()
        {
            Succeeded = true,
            Data = data,
            Message = message
        };

    public static Result<T> Failure(string message, string? errorCode = null, IEnumerable<string>? errors = null)
        => new()
        {
            Succeeded = false,
            Message = message,
            ErrorCode = errorCode,
            Errors = errors
        };
}