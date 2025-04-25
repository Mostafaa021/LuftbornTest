using Luftborn.Application.DTOs;
using Mapster;

namespace Luftborn.Application.Common.Factories;

// Factory Interface
public interface IProductFactory
{
    Core.DomainEntities.Product CreateProduct(ProductDto dto);
}

// Concrete Implementation 1
public class RegularProductFactory : IProductFactory 
{
    public Core.DomainEntities.Product CreateProduct(ProductDto dto)
    {
        var product = dto.Adapt<Core.DomainEntities.Product>();
        product.IsRegular = true;
        return product;
    }
}

// Concrete Implementation 2
public class PromotionalProductFactory : IProductFactory
{
    public Core.DomainEntities.Product CreateProduct(ProductDto dto)
    {
        var product = dto.Adapt<Core.DomainEntities.Product>();
        product.IsPromotional = true;
        product.DiscountPrecentage = 10;
        return product;
    }
}

// Factory Provider
public class ProductFactoryProvider
{
    public static IProductFactory GetProductFactory(string productType)
    {
        return productType.ToLower() switch
        {
            "promotional" => new PromotionalProductFactory(),
            _ => new RegularProductFactory()
        };
    }
}