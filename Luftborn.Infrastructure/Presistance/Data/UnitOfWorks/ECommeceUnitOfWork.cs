using Luftborn.Core.Abstraction.Domain;
using Luftborn.Core.Abstraction.Repostories;
using Luftborn.Infrastructure.Presistance.Data.Context;
using Luftborn.Infrastructure.Presistance.Data.Repositories;
using Luftborn.Infrastructure.Presistance.Data.Repositories.MainRepo;

namespace Luftborn.Infrastructure.Presistance.Data.UnitOfWorks;

public class ECommeceUnitOfWork : UnitOfWork<ECommerceDbContext>, IECommerceUnitOfWork
{
    public ECommeceUnitOfWork(ECommerceDbContext context) : base(context) { }
    
    private ProductRepository _prodcuts;
    public IProductRepository Products => _prodcuts ??= new ProductRepository(context: Context);
    private CategoryRepository _categories;
    public ICategoryRepository Categories => _categories ??= new CategoryRepository(context: Context);
    private OrderRepository _orders;
    public IOrderRepository Orders => _orders ??= new OrderRepository(context: Context);
    private OrderItemRepository _orderitems;
    public IOrderItemRepository OrderItems => _orderitems ??= new OrderItemRepository(context: Context);
    private CustomerRepository _customers;
    public ICustomerRepository Customers => _customers ??= new CustomerRepository(context: Context);
    
    public override IRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        return new Repository<TEntity, ECommerceDbContext>(Context);
    }
}
