using HRMS.Application;
using HRMS.Application.Common.Interfaces;
using HRMS.Infrastructure;
using HRMS.Infrastructure.Identity;
using HRMS.Infrastructure.Localization;
using HRMS.Infrastructure.Persistence;
using HRMS.Infrastructure.Persistence.Seeds;
using HRMS.WebApi.Extensions;
using HRMS.WebApi.Middlewares;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Localization;
using Serilog;
using System.Globalization;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

    // Services
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerConfiguration();
    builder.Services.AddApiVersioningConfiguration();
    builder.Services.AddHealthCheckConfiguration(builder.Configuration);
    builder.Services.AddCorsConfiguration(builder.Configuration);
    builder.Services.AddAuthorization();

    builder.Services.AddLocalization();

    builder.Services.AddScoped<ILocalizationService, LocalizationService>();

    var app = builder.Build();

    // Seed Database
    using (var scope = app.Services.CreateScope())
    {   
        var context = scope.ServiceProvider.GetRequiredService<HrmsDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await context.Database.EnsureCreatedAsync();
        await DataSeeder.SeedAsync(context, userManager, roleManager);
    }


    var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ar")
};

    app.UseRequestLocalization(new RequestLocalizationOptions
    {
        DefaultRequestCulture = new RequestCulture("en"),
        SupportedCultures = supportedCultures,
        SupportedUICultures = supportedCultures
    });

    // Middleware pipeline
    app.UseMiddleware<CorrelationIdMiddleware>();
    app.UseMiddleware<SecurityHeadersMiddleware>();
    app.UseMiddleware<RequestLoggingMiddleware>();
    app.UseMiddleware<GlobalExceptionMiddleware>();

    app.UseSwaggerConfiguration();
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();
    app.UseCors("HRMSCorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHealthCheckConfiguration();
    app.MapControllers();

    Log.Information("HRMS API starting on {Environment}", app.Environment.EnvironmentName);
    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

// Required for integration test factory
public partial class Program { }
