namespace Catalog.API.Configurations;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder Create(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder
            .Services
            .AddCatalogServices()
            .AddCatalogOptions();
        return builder;
    }
}
