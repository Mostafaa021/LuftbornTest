namespace Luftborn.Core.Abstraction.Domain;

public interface IPagedList<TEntity>
    where TEntity : class
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public IEnumerable<TEntity> Entities { get; set; }
}