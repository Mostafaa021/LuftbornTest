using Luftborn.Core.Abstraction;

namespace Luftborn.Core.DomainEntities;

public sealed class Category : IEntity
{
    public int Id { get; set; }
    public required string Name { get; set; } // may be renamed by admin 
    public string?  Description { get; set; } // editable by admin
    public bool IsDeleted { get; set; }
    public ICollection<Product> Products { get; init; } = new List<Product>();
 
    
}