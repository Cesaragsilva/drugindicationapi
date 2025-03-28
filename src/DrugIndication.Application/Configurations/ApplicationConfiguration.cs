using DrugIndication.Application.Interfaces.Services;
using DrugIndication.Application.Services;
using DrugIndication.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DrugIndication.Application.Configurations
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddServices()
                    .AddValidators();

            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddScoped<IUserService, UserService>()
                .AddScoped<IJwtService, JwtService>()
                .AddScoped<IProgramService, ProgramService>();

            return services;
        }

        private static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<UserDtoValidator>();

            return services;
        }
    }
}
