using Luftborn.Core.Abstraction.Repostories;
using Luftborn.Core.DomainEntities;
using Luftborn.Infrastructure.Presistance.Data.Context;
using Luftborn.Infrastructure.Presistance.Data.Repositories.MainRepo;

namespace Luftborn.Infrastructure.Presistance.Data.Repositories;

public class OrderItemRepository(ECommerceDbContext context)
    : Repository<OrderItem, ECommerceDbContext>(context), IOrderItemRepository;