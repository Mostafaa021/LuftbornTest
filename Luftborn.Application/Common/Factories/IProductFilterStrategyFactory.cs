using Luftborn.Application.Common.Strategies;

namespace Luftborn.Application.Common.Factories;

// factory for creating strategy 
public interface IProductFilterStrategyFactory
{
    IProductFilterStrategy GetStrategy(string strategyType);
}

public class ProductFilterStrategyFactory : IProductFilterStrategyFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ProductFilterStrategyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IProductFilterStrategy GetStrategy(string strategyType)
    {
        return strategyType.ToLower() switch
        {
            "active" => new ActiveProductsStrategy(),
            "deleted" => new DeletedProductsStrategy(),
        };
    }
}