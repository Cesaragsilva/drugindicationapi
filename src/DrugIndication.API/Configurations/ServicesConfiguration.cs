using DrugIndication.Api.Filter;
using DrugIndication.Application.Configurations;
using DrugIndication.Application.Dtos;
using DrugIndication.Application.Model;
using DrugIndication.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DrugIndication.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.ConfigureBinding(configuration)
                    .ConfigureAuthentication(configuration);

            services.AddAuthorization();

            services.AddControllers(options =>
            {
                options.AddModelValidationFilter<CreateUserDto>()
                       .AddModelValidationFilter<UserDto>();
            });

            services.AddHealthChecks();

            services.AddEndpointsApiExplorer();

            services.ConfigureApplicationServices()
                    .ConfigureInfrastructure(configuration);

            services.ConfigureSwaggerDocumentation();

            return services;
        }

        private static void ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!)
                            )
                        };
                    });
        }

        private static IServiceCollection ConfigureBinding(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

            return services;
        }

        private static MvcOptions AddModelValidationFilter<T>(this MvcOptions options) where T : class
        {
            options.Filters.Add(typeof(ModelValidationFilter<T>));

            return options;
        }
    }
}
