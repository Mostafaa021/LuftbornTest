using Luftborn.Core.Abstraction.Repostories;
using Luftborn.Core.DomainEntities;
using Luftborn.Infrastructure.Presistance.Data.Context;
using Luftborn.Infrastructure.Presistance.Data.Repositories.MainRepo;

namespace Luftborn.Infrastructure.Presistance.Data.Repositories;

public class CustomerRepository(ECommerceDbContext context)
    : Repository<Customer, ECommerceDbContext>(context), ICustomerRepository;