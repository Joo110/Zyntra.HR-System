namespace HRMS.WebApi.Extensions;
public static class CorsExtensions
{
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("HRMSCorsPolicy", builder =>
                builder.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>() ?? new[] { "http://localhost:3000", "https://localhost:7001" })
                    .AllowAnyMethod().AllowAnyHeader().AllowCredentials());
        });
        return services;
    }
}
