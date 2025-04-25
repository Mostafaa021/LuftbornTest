using System.Linq.Expressions;
using Luftborn.Core.Abstraction.Domain;
using Luftborn.Core.DomainEntities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Luftborn.Infrastructure.Presistance.Data.Repositories.MainRepo;


public class Repository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext, IDisposable
{
    protected TContext context;

    public Repository(TContext context)
    {
        this.context = context;
    }

    #region Get

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="include"></param>
    /// <returns></returns>
    public virtual TEntity Get(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (include != null)
        {
            query = include(query).AsSplitQuery();
        }

        return query.FirstOrDefault(predicate);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relatedEntities"></param>
    /// <returns></returns>
    public virtual TEntity Get(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        if (relatedEntities != null)
        {
            return relatedEntities
                .Aggregate(
                    context.Set<TEntity>().AsQueryable(),
                    (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy))
                .FirstOrDefault(predicate);
        }

        return context.Set<TEntity>().FirstOrDefault(predicate);
    }

    // make function that return IQueryable<TEntity> 
    
    
    /// <summary>
    /// Get By Predicate With Includable
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relatedEntities"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (include != null)
        {
            query = include(query).AsSplitQuery();
        }

        return await query.FirstOrDefaultAsync(predicate);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relatedEntities"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        if (relatedEntities != null)
        {
            return await relatedEntities
                .Aggregate(
                    context.Set<TEntity>().AsQueryable(),
                    (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy))
                .FirstOrDefaultAsync(predicate);
        }

        return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
    }
    
    /// <summary>
    /// Returns IQueryable to allow custom queries to be built
    /// </summary>
    /// <param name="predicate">Optional filter predicate</param>
    /// <param name="include">Optional related entities to include</param>
    /// <returns>IQueryable of entities that can be further filtered or projected</returns>
    public virtual IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
    
        if (include != null)
        {
            query = include(query);
        }
    
        if (predicate != null)
        {
            query = query.Where(predicate);
        }
    
        return query;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relatedEntities"></param>
    /// <returns></returns>
    public virtual TEntity GetSingle(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        if (relatedEntities != null)
        {
            return relatedEntities
                .Aggregate(
                    context.Set<TEntity>().AsQueryable(),
                    (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy))
                .SingleOrDefault(predicate);
        }

        return context.Set<TEntity>().SingleOrDefault(predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="relatedEntities"></param>
    /// <returns></returns>
    public virtual async Task<TEntity> GetSingleAsync(
        Expression<Func<TEntity, bool>> predicate,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        if (relatedEntities != null)
        {
            return await relatedEntities
                .Aggregate(
                    context.Set<TEntity>().AsQueryable(),
                    (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy))
                .SingleOrDefaultAsync(predicate);
        }

        return await context.Set<TEntity>().SingleOrDefaultAsync(predicate);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="count">Count of the returned items. If count <= 0 then all items are returned</param>
    /// <param name="orderBy"></param>
    /// <param name="relatedEntities"></param>
    /// <returns>Returns top n items where n = count</returns>
    public virtual IEnumerable<TEntity> GetList(
        Expression<Func<TEntity, bool>> predicate = null,
        int count = 0,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        IQueryable<TEntity> query;

        if (relatedEntities != null)
        {
            query = relatedEntities.Aggregate(
                context.Set<TEntity>().AsQueryable(),
                (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy));
        }
        else query = context.Set<TEntity>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
            query = orderBy(query);

        if (count > 0)
        {
            query = query.Take(count);
        }

        return query.ToList();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="count">Count of the returned items. If count <= 0 then all items are returned</param>
    /// <param name="orderBy"></param>
    /// <param name="relatedEntities"></param>
    /// <returns>Returns top n items where n = count</returns>
    public virtual async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        int count = 0,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        IQueryable<TEntity> query;

        if (relatedEntities != null)
        {
            query = relatedEntities.Aggregate(
                context.Set<TEntity>().AsQueryable(),
                (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy));
        }
        else query = context.Set<TEntity>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
            query = orderBy(query);

        if (count > 0)
        {
            query = query.Take(count);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="count">Count of the returned items. If count <= 0 then all items are returned</param>
    /// <param name="orderBy"></param>
    /// <param name="include"></param>
    /// <returns>Returns top n items where n = count</returns>
    public virtual async Task<IEnumerable<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        int count = 0,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();
        if (include != null)
        {
            query = include(query);
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        if (orderBy != null)
            query = orderBy(query);

        if (count > 0)
        {
            query = query.Take(count);
        }

        return await query.ToListAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="relatedEntities"></param>
    /// <returns></returns>
    public virtual PagedList<TEntity> GetPagedList(
        Expression<Func<TEntity, bool>> predicate ,
        int pageNumber = 0,
        int pageSize = 0,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        IQueryable<TEntity> query;

        if (relatedEntities != null)
        {
            query = relatedEntities.Aggregate(
                context.Set<TEntity>().AsQueryable(),
                (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy));
        }
        else query = context.Set<TEntity>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        int count = query.Count();

        if (orderBy != null)
            query = orderBy(query);

        if (pageNumber > 0 && pageSize > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        var entities = query.ToList();

        return new PagedList<TEntity>
        {
            TotalCount = count,
            PageNumber = pageNumber <= 0 ? 1 : pageNumber,
            PageSize = pageSize <= 0 ? count : pageSize,
            Entities = entities
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="relatedEntities"></param>
    /// <returns></returns>
    public virtual async Task<PagedList<TEntity>> GetPagedListAsync(
        Expression<Func<TEntity, bool>> predicate ,
        int pageNumber = 0,
        int pageSize = 0,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        params Expression<Func<TEntity, object>>[] relatedEntities)
    {
        IQueryable<TEntity> query;

        if (relatedEntities != null)
        {
            query = relatedEntities.Aggregate(
                context.Set<TEntity>().AsQueryable(),
                (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy));
        }
        else query = context.Set<TEntity>();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        int count = await query.CountAsync();

        if (orderBy != null)
            query = orderBy(query);

        if (pageNumber > 0 && pageSize > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        var entities = await query.ToListAsync();

        return new PagedList<TEntity>
        {
            TotalCount = count,
            PageNumber = pageNumber <= 0 ? 1 : pageNumber,
            PageSize = pageSize <= 0 ? count : pageSize,
            Entities = entities
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="orderBy"></param>
    /// <param name="include"></param>
    /// <returns></returns>
    public virtual async Task<PagedList<TEntity>> GetPagedListAsync(
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber = 0,
        int pageSize = 0,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
    {
        IQueryable<TEntity> query = context.Set<TEntity>();

        if (include != null)
        {
            query = include(query.AsSplitQuery());
        }

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        int count = await query.CountAsync();

        if (orderBy != null)
            query = orderBy(query);

        if (pageNumber > 0 && pageSize > 0)
        {
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        var entities = await query.ToListAsync();

        return new PagedList<TEntity>
        {
            TotalCount = count,
            PageNumber = pageNumber <= 0 ? 1 : pageNumber,
            PageSize = pageSize <= 0 ? count : pageSize,
            Entities = entities
        };
    }



#endregion

        #region Filters
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().AsNoTracking().Any(predicate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().AsNoTracking().AnyAsync(predicate);
        }
        #endregion

        #region Aggregation
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual T Max<T>(Func<TEntity, T> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
        {
            return context.Set<TEntity>().Max(selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual async Task<T> MaxAsync<T>(Expression<Func<TEntity, T>> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
        {
            return await context.Set<TEntity>().MaxAsync(selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual T Min<T>(Func<TEntity, T> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
        {
            return context.Set<TEntity>().Min(selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual async Task<T> MinAsync<T>(Expression<Func<TEntity, T>> selector)
            where T : struct, IComparable, IComparable<T>, IFormattable, IConvertible, IEquatable<T>
        {
            return await context.Set<TEntity>().MinAsync(selector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual int Count()
        {
            return context.Set<TEntity>().Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="relatedEntities"></param>
        /// <returns></returns>
        public virtual int Count(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities)
        {
            IQueryable<TEntity> query;

            if (relatedEntities == null)
            {
                query = relatedEntities.Aggregate(
                    context.Set<TEntity>().AsQueryable(),
                    (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy));
            }
            else query = context.Set<TEntity>();

            if (predicate != null)
                return query.Count(predicate);
            else return query.Count();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="relatedEntities"></param>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(
            Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] relatedEntities)
        {
            IQueryable<TEntity> query;

            if (relatedEntities == null)
            {
                query = relatedEntities.Aggregate(
                    context.Set<TEntity>().AsQueryable(),
                    (relatedEntitiesQuery, relatedEntitiy) => relatedEntitiesQuery.Include(relatedEntitiy));
            }
            else query = context.Set<TEntity>();

            if (predicate != null)
                return await query.CountAsync(predicate);
            else return await query.CountAsync();
        }
        #endregion

        #region Add
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual void Add(TEntity entity)
        {
            context.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            await context.AddAsync(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            context.AddRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await context.AddRangeAsync(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newValue"></param>
        public virtual void Update(TEntity entity)
        {
            context.Update(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        public virtual void UpdateRange(TEntity[] entities)
        {
            context.UpdateRange(entities);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedEntity"></param>
        public virtual void UpdateEntry(object id, object updatedEntity)
        {
            var entity = context.Find<TEntity>(id);
            context.Entry<TEntity>(entity).CurrentValues.SetValues(updatedEntity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="updatedEntity"></param>
        public virtual void UpdateEntry(TEntity entity, object updatedEntity)
        {
            context.Entry<TEntity>(entity).CurrentValues.SetValues(updatedEntity);
        }

        #endregion

        #region Remove

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public virtual void Remove(object id)
        {
            var entity = context.Find<TEntity>(id);
            context.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Remove(TEntity entity)
        {
            context.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            context.RemoveRange(entities);
        }

        #endregion

        protected virtual IQueryable<TEntity> GetQueryable()
        {
            return context.Set<TEntity>().AsQueryable();
        }
    }
    