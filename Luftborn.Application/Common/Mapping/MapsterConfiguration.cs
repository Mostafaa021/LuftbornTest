using Luftborn.Application.DTOs;
using Luftborn.Core.DomainEntities;
using Mapster;

namespace Luftborn.Application.Common.Mapping;

public static class MapsterConfiguration
{
    public static void RegisterMappings()
    {
        // Global config
        TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true)
            .IgnoreNullValues(true);

        // Entity to DTO
        TypeAdapterConfig<Product, ProductDto>.NewConfig()
            .TwoWays()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.CategoryName, src => src.Category.Name)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.IsActive, src => src.IsActive)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);





        // DTO to Entity (for create/update)
        TypeAdapterConfig<ProductDto, Product>.NewConfig()
            .Ignore(dest => dest.Id) // Id handled by EF
            .Ignore(dest => dest.Category) // navigation
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Price, src => src.Price)
            .Map(dest => dest.IsActive, src => src.IsActive)
            .Map(dest => dest.Category.Name, src => src.CategoryName);


    }
}