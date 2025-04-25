using Luftborn.Core.Abstraction.Repostories;

namespace Luftborn.Core.Abstraction.Domain;

public interface IECommerceUnitOfWork : IUnitOfWork
{
    public IProductRepository Products { get; }
    public ICategoryRepository Categories { get; }
    public ICustomerRepository Customers { get; }
    public IOrderRepository Orders { get; }
    public IOrderItemRepository OrderItems { get; }
        
}