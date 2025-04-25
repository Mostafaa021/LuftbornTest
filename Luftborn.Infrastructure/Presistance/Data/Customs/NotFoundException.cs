namespace Luftborn.Infrastructure.Presistance.Data.Customs;

public class NotFoundException : CustomException
{
    public NotFoundException() : base()
    {

    }

    public NotFoundException(string message) : base(message)
    {

    }

    public override string Message => message;
}

public class NotFoundException<TEntity> : NotFoundException
{
    public NotFoundException()
    {

    }

    public NotFoundException(string message) : base(message)
    {

    }

    public static NotFoundException Create()
    {
        return new NotFoundException<TEntity>($"{nameof(TEntity)} not found");
    }

    public override string Message => message;
}