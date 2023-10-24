using Catalog.API.Repositories;
namespace Catalog.API.Configurations;
public static class Registration
{
    public static IServiceCollection AddCatalogServices(this IServiceCollection services)
    {
        services
           .AddSingleton<ICatalogContext, CatalogContext>()
           .AddSingleton<IProductRepository, ProductRepository>()
           .AddEndpointsApiExplorer()
           .AddSwaggerGen()
           .AddControllers();
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
}
