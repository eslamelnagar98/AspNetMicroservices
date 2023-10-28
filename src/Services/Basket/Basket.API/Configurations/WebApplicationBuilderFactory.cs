namespace Basket.API.Configurations;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder Create(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .Services
            .AddCatalogOptions()
            .AddBasketServices();
        return builder;
    }
}
