namespace Luftborn.Core.Abstraction;

public interface IEntity
{
    int Id { get; set; } // Or Guid in case Using GUID 
    bool IsDeleted { get; set; } 
}