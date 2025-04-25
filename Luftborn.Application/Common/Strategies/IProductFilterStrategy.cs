using Luftborn.Application.Common.Results;
using Luftborn.Application.DTOs;
using Luftborn.Core.Abstraction.Domain;
using Luftborn.Core.DomainEntities;
using MapsterMapper;

namespace Luftborn.Application.Common.Strategies;


//  Strategy Abstraction
public interface IProductFilterStrategy
{
    Task<IQueryable<Product>> ApplyFilter(IQueryable<Product> products);
}

// Concrete Strategy 1
public class ActiveProductsStrategy : IProductFilterStrategy
{
    public  Task<IQueryable<Product>> ApplyFilter(IQueryable<Product> products)
    {
        return Task.FromResult(products.Where(x => x.IsActive));
    }
}
// Concrete Strategy 2
public class DeletedProductsStrategy : IProductFilterStrategy
{
    public Task<IQueryable<Product>> ApplyFilter(IQueryable<Product> products)
    {
        return Task.FromResult(products.Where(x => x.IsDeleted));
    }
}
