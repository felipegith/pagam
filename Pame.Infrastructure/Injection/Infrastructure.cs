using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pame.Domain;

namespace Pame.Infrastructure.Injection;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IPayableRepository, PayableRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<AppContext>(options => options.UseInMemoryDatabase("Database"));

        services.AddScoped<PublishDomainEventInterceptor>();
        return services;
    }
}
