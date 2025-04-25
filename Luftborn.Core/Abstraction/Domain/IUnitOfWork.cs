namespace Luftborn.Core.Abstraction.Domain;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;

    int Complete();
    Task<int> CompleteAsync();
    void BeginTransaction(); // for transaction management if i will not use unit of work
    void RollBackTransaction(); // for transaction management if i will not use unit of work
    void CommitTransaction();// for transaction management if i will not use unit of work
}

public interface IUnitOfWork<TContext> : IDisposable 
    where TContext: IDisposable 
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    TContext Context { get; }

    int Complete();
    Task<int> CompleteAsync();
    void BeginTransaction(); // for transaction management if i will not use unit of work
    void RollBackTransaction();// for transaction management if i will not use unit of work
    void CommitTransaction();// for transaction management if i will not use unit of work
}