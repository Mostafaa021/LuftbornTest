using Luftborn.Core.Abstraction;

namespace Luftborn.Core.DomainEntities;

public sealed class OrderItem : IEntity
{
    public int Id { get; set; } // PK
    
    public int Quantity { get; set; } // Might adjust
    public decimal UnitPrice { get; set; }   // Might adjust in admin
    
    public decimal TotalPrice => Quantity * UnitPrice; // Computed
    
    public int OrderId { get; init; } // FK
    public Order Order { get; init; }

    public int ProductId { get; init; } // FK
    public Product Product { get; init; }
    
    public bool IsDeleted { get; set; } 
}