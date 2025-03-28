using DrugIndication.Middlewares;

namespace DrugIndication.Configurations
{
    public static class ApiConfiguration
    {
        public static WebApplication ConfigureApi(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerConfiguration();
            }

            app.UseCors("AllowAll");
            app.UseMiddleware<ExceptionMiddleware>()
               .UseHealthChecks("/health")
               .UseHttpsRedirection()
               .UseAuthentication()
               .UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
