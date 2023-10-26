namespace Catalog.API.Configurations;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder Create(string[] args)
    {
        Console.WriteLine(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        var builder = WebApplication.CreateBuilder(args);
        builder
            .Services
            .AddCatalogServices()
            .AddCatalogOptions();
        return builder;
    }
}
