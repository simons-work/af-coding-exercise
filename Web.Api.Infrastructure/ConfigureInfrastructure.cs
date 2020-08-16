using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Api.Infrastructure.Data;
using Web.Api.Infrastructure.Data.Repository;

namespace Web.Api.Infrastructure
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection AddConnectionProvider(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddDbContext<InsuranceDbContext>(cfg => { cfg.UseSqlServer(configuration.GetConnectionString("InsuranceDb")); });
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}