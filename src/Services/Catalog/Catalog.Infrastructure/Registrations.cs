using Catalog.API.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class Registrations
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddDbContext<CatalogContext>();

            return services;
        }
    }
}
