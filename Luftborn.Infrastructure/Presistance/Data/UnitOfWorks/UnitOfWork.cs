using System.Data;
using Luftborn.Core.Abstraction.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Luftborn.Infrastructure.Presistance.Data.UnitOfWorks;

public abstract class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext, IDisposable
{
    readonly TContext _context;
    private IDbContextTransaction _dbContextTransaction { get; set; }

    protected UnitOfWork(TContext context)
    {
        _context = context; 
    }

    #region Methods
    public abstract IRepository<TEntity> Repository<TEntity>() where TEntity : class;

    protected TContext Context => _context;

    public virtual void BeginTransaction()
    {
        _dbContextTransaction = Context.Database.BeginTransaction(IsolationLevel.Serializable);
    }

    public virtual void RollBackTransaction()
    {
        _dbContextTransaction.Rollback();
        _dbContextTransaction.Dispose();
    }

    public virtual void CommitTransaction()
    {
        _dbContextTransaction.Commit();
        _dbContextTransaction.Dispose();
    }

    public virtual int Complete()
    {
        return Context.SaveChanges();
    }

    public virtual async Task<int> CompleteAsync()
    {
        return await Context.SaveChangesAsync();
    }

    #region Dispose
    private bool _disposed ;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            Context.Dispose();
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    #endregion

    #endregion
}
