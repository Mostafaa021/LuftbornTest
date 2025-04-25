using Luftborn.Core.Abstraction;

namespace Luftborn.Core.DomainEntities;

public sealed class Order : IEntity
{
    public int Id { get; set; }// PK
    
    public DateTime OrderDate { get; init; } // set once 
    public decimal TotalAmount { get; set; } // May be recalculated 
    public OrderStatus Status { get; set; }  // change as order progresses
    
    
    public int CustomerId { get; init; } // FK
    public Customer Customer { get; init; }
    

    public bool IsDeleted { get; set; } 

    public ICollection<OrderItem> OrderItems { get; init; } = new List<OrderItem>();
}