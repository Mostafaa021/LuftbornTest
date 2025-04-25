namespace Luftborn.Application.DTOs;

public record ProductDto
{
    public int Id { get; set; }
    public required string Name { get; set; } 
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public bool IsActive { get; set; }
    
    public required string ProductType { get; set; }
    public  required string CategoryName { get; set; } 
}