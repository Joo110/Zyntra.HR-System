namespace HRMS.WebApi.Extensions;
public static class CorsExtensions
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("HRMSCorsPolicy", builder =>
                builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:5173", "https://localhost:5173" })
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
        });
        return services;
    }
}
