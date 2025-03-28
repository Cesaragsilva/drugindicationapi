using DrugIndication.Application.Interfaces.Repositories;
using DrugIndication.Infrastructure.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrugIndication.Infrastructure.Configurations
{
    public static class InfrastructureConfigurations
    {
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<IProgramRepository, ProgramRepository>()
                .AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
