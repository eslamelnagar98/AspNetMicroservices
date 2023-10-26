﻿namespace Catalog.API.Configurations;
public static class Registration
{
    public static async Task RunCatalogAsync(this WebApplicationBuilder builder)
    {
        var app = default(WebApplication);
        try
        {
            app = builder.Build();
            app.UseSwagger()
               .UseSwaggerUI()
               .UseAuthorization();
            app.MapControllers();
            await app.RunAsync();
        }
        catch (Exception)
        {
            await app.StopAsync();
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
