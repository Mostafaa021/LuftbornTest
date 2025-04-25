
namespace Luftborn.Infrastructure.Presistance.Data.Customs;

public abstract class CustomException : Exception
{
    protected CustomException()
    {
        message = string.Empty;
    }

    protected CustomException(string message)
    {
        this.message = message;
    }

    protected CustomException(string errorCode, string message)
    {
        this.message = message;
        this.errorCode = errorCode;
    }

    protected string message;
    private readonly string errorCode;

    public virtual string ErrorCode => errorCode;
}