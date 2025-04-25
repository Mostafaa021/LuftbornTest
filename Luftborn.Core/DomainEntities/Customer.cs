using Luftborn.Core.Abstraction;

namespace Luftborn.Core.DomainEntities;

public sealed class Customer : IEntity
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; init; } // Shouldn't change usually
    public string PhoneNumber { get; set; } // might be updated 
    public string Address { get; set; } // might be updated
    public bool IsDeleted { get; set; }

    public ICollection<Order> Orders { get; init; } = new List<Order>();
}