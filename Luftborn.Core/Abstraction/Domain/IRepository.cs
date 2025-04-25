using System.Linq.Expressions;
using Luftborn.Core.DomainEntities.Shared;
using Microsoft.EntityFrameworkCore.Query;

namespace Luftborn.Core.Abstraction.Domain;

 public interface IRepository<TEntity>
        where TEntity : class
    {
        #region Get
        TEntity Get(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        TEntity Get(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        TEntity GetSingle(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        Task<TEntity> GetSingleAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        IEnumerable<TEntity> GetList(
            Expression<Func<TEntity, bool>> predicate = null,
            int count = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);
        
        
        IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            int count = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> predicate = null,
            int count = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        PagedList<TEntity> GetPagedList(
            Expression<Func<TEntity, bool>> predicate,
            int pageNumber = 0,
            int pageSize = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        Task<PagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate,
            int pageNumber = 0,
            int pageSize = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] relatedEntities);

        Task<PagedList<TEntity>> GetPagedListAsync(
            Expression<Func<TEntity, bool>> predicate,
            int pageNumber = 0,
            int pageSize = 0,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        

        #endregion

        #region Filters
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion

        #region Aggregation
        T Max<T>(Func<TEntity, T> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        T Min<T>(Func<TEntity, T> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>;
        int Count();
        int Count(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);
        Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities);
        #endregion

        #region Add
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(TEntity[] entities);
        void UpdateEntry(object id, object updatedEntity);
        void UpdateEntry(TEntity entity, object updatedEntity);
        #endregion

        #region Remove
        void Remove(object id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        #endregion

    }