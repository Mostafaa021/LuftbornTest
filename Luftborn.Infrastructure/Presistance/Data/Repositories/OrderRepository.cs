using Luftborn.Core.Abstraction.Repostories;
using Luftborn.Core.DomainEntities;
using Luftborn.Infrastructure.Presistance.Data.Context;
using Luftborn.Infrastructure.Presistance.Data.Repositories.MainRepo;

namespace Luftborn.Infrastructure.Presistance.Data.Repositories;

public class OrderRepository(ECommerceDbContext context)
    : Repository<Order, ECommerceDbContext>(context), IOrderRepository;
