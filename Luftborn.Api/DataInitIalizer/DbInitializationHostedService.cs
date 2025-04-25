using Luftborn.Infrastructure.Presistance.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LuftbornTestApp.DataInitIalizer;

public class DataInitializationHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();
        await dbContext.Database.MigrateAsync(cancellationToken);
        await DataInitialzer.SeedAsync(dbContext , cancellationToken);
    }


    public  Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}