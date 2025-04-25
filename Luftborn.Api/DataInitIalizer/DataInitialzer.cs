using Luftborn.Core.DomainEntities;
using Luftborn.Infrastructure.Presistance.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LuftbornTestApp.DataInitIalizer;

public  static class DataInitialzer
{
   public static async Task SeedAsync(ECommerceDbContext context, CancellationToken cancellationToken)
        {
            // Apply migrations if not applied
            await context.Database.MigrateAsync(cancellationToken);
            // Seed data if not already present
            await SeedCategoriesAsync(context, cancellationToken);
            await SeedProductsAsync(context, cancellationToken);
            await SeedCustomersAsync(context, cancellationToken);
            await SeedOrdersAsync(context, cancellationToken);
            await SeedOrderItemsAsync(context, cancellationToken);
        }
   private static async Task SeedCategoriesAsync(ECommerceDbContext context, CancellationToken cancellationToken)
    {
        if (!await context.Categories.AnyAsync(cancellationToken))
        {
          await context.Categories.AddRangeAsync(
                new Category { Name = "Electronics", Description = "Gadgets and devices", IsDeleted = false },
                new Category {  Name = "Books", Description = "Various kinds of books", IsDeleted = false }
            );
            await context.SaveChangesAsync(cancellationToken);
        }
        
    }

    private static async Task SeedProductsAsync(ECommerceDbContext context, CancellationToken cancellationToken)
    {
        if ( !await context.Products.AnyAsync(cancellationToken))
        {
           await context.Products.AddRangeAsync(
                // Regular product
                new Product
                {
                    Name = "Smartphone X",
                    Description = "Flagship 5G smartphone with AMOLED display",
                    Price = 799.99m,
                    DiscountPrecentage = 0m,
                    IsPromotional = false,
                    IsRegular = true,
                    StockQuantity = 50,
                    ImageUrl = "smartphone_x.jpg",
                    IsActive = true,
                    IsDeleted = false,
                    CategoryId = 3
                },
                // Promotional product
                new Product
                {
                    Name = "Laptop Pro 15",
                    Description = "High performance laptop for professionals",
                    Price = 1499.99m,
                    DiscountPrecentage = 15m,
                    IsPromotional = true,
                    IsRegular = false,
                    StockQuantity = 30,
                    ImageUrl = "laptop_pro_15.jpg",
                    IsActive = true,
                    IsDeleted = false,
                    CategoryId = 3
                },
                new Product
                {
                    Name = "Science Encyclopedia",
                    Description = "Comprehensive encyclopedia of science topics",
                    Price = 39.99m,
                    DiscountPrecentage = 0m,
                    IsPromotional = false,
                    IsRegular = true,
                    StockQuantity = 100,
                    ImageUrl = "science_encyclopedia.jpg",
                    IsActive = true,
                    IsDeleted = false,
                    CategoryId = 4
                },
                new Product
                {
                    Name = "Data Intensive Application",
                    Description = "Data Intensive Application book",
                    Price = 39.99m,
                    DiscountPrecentage = 0m,
                    IsPromotional = true,
                    IsRegular = false,
                    StockQuantity = 100,
                    ImageUrl = "Data_Intensive_app.jpg",
                    IsActive = true,
                    IsDeleted = false,
                    CategoryId = 4
                }
            );
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task SeedCustomersAsync(ECommerceDbContext context, CancellationToken cancellationToken)
    {
        if (!await context.Customers.AnyAsync(cancellationToken))
        {
           await context.Customers.AddRangeAsync(
                new Customer {  FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", PhoneNumber = "123-456-7890", Address = "123 Main St", IsDeleted = false },
                new Customer {  FirstName = "Bob", LastName = "Jones", Email = "bob@example.com", PhoneNumber = "234-567-8901", Address = "456 Oak Ave", IsDeleted = false }
            );
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task SeedOrdersAsync(ECommerceDbContext context, CancellationToken cancellationToken)
    {
        if (!await context.Orders.AnyAsync(cancellationToken))
        {
          await  context.Orders.AddRangeAsync(
                new Order {  CustomerId = 3, OrderDate = DateTime.UtcNow.AddDays(-7), TotalAmount = 1429.98m, Status = OrderStatus.Delivered, IsDeleted = false },
                new Order {  CustomerId = 4, OrderDate = DateTime.UtcNow.AddDays(-3), TotalAmount = 29.99m, Status = OrderStatus.Pending, IsDeleted = false }
            );
            await context.SaveChangesAsync(cancellationToken);
        }
    }

    private static async Task SeedOrderItemsAsync(ECommerceDbContext context, CancellationToken cancellationToken)
    {
        if ( ! await context.OrderItems.AnyAsync(cancellationToken))
        {
           await  context.OrderItems.AddRangeAsync(
                new OrderItem {  OrderId = 4, ProductId = 7, Quantity = 1, UnitPrice = 699.99m, IsDeleted = false },
                new OrderItem {  OrderId = 5, ProductId = 8, Quantity = 1, UnitPrice = 729.99m, IsDeleted = false },
                new OrderItem {  OrderId = 5, ProductId = 8, Quantity = 1, UnitPrice = 29.99m, IsDeleted = false }
            );
            await context.SaveChangesAsync(cancellationToken);
        }
    }
    
}