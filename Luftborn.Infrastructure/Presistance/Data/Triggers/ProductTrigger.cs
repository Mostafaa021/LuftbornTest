using EntityFrameworkCore.Triggered;
using Luftborn.Core.DomainEntities;
using Microsoft.Extensions.Logging;

namespace Luftborn.Infrastructure.Presistance.Data.Triggers;

public class ProductTrigger : IAfterSaveTrigger<Product>
{
    private readonly ILogger<ProductTrigger> _logger;

    public ProductTrigger(ILogger<ProductTrigger> logger)
    {
        _logger = logger;
    }

    public async Task AfterSave(ITriggerContext<Product> context, CancellationToken cancellationToken)
    {
        switch (context)
        {
            // here we can log the product after saving and use audit trail to track changes
            case { ChangeType: ChangeType.Added }:
                _logger.LogInformation("Product added: {Product}", context.Entity);
                break;
            case { ChangeType: ChangeType.Deleted }:
                _logger.LogInformation("Product deleted: {Product}", context.Entity);
                break;
            case { ChangeType: ChangeType.Modified }:
                _logger.LogInformation("Product modified: {Product}", context.Entity);
                break;
        }
    }
}