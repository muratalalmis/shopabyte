using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Services;

namespace Ordering.Infrastructure
{
    public static class Registrations
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddDbContext<OrderContext>();

            services.AddScoped<ISalesRepository, SalesRepository>();

            services.AddTransient<IEmailService, DefaultEmailService>();

            return services;
        }
    }
}
