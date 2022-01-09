using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure
{
    public static class Registrations
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddDbContext<OrderContext>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepositoryBase<>));
            services.AddScoped<ISalesRepository, SalesRepository>();

            return services;
        }
    }
}
