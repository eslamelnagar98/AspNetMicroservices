﻿namespace Catalog.API.Configurations;
public static class Registration
{
    public static async Task RunCatalogAsync(this WebApplicationBuilder builder)
    {
        var logger = default(Logger);
        var serviceName = string.Empty;
        var app = default(WebApplication);
        try
        {
            logger = Extension.GetLogger();
            serviceName = Extension.GetServiceName();
            app = builder.Build();
            logger.Info($"Service {serviceName} Starts Successfully");
            await app.AddMiddlewares()
                     .RunAsync();
        }
        catch (Exception exception)
        {
            logger?.Error(exception, $"Service {serviceName} Stopping Due To Exception");
            await app?.StopAsync();
        }
        finally
        {
            LogManager.Shutdown();
        }
    }
    public static IServiceCollection AddCatalogServices(this IServiceCollection services)
    {
        services
           .AddScoped<ICatalogContext, CatalogContext>()
           .AddScoped<IProductRepository, ProductRepository>()
           .AddControllerConfigurations();
        return services;
    }

    public static IServiceCollection AddCatalogOptions(this IServiceCollection services)
    {
        services
            .AddOptions<DatabaseSettingsOptions>()
            .Configure<IConfiguration>(
            (options, configuration) =>
             configuration.GetSection(DatabaseSettingsOptions.SectionName)
            .Bind(options))
            .ValidateOnStart();
        return services;
    }

    private static IServiceCollection AddControllerConfigurations(this IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddControllers();
        return services;
    }
}
