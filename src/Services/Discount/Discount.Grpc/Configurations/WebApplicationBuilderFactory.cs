namespace Discount.Grpc.Configurations;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateDiscountWebBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .Services
            .AddDiscountOptions()
            .AddDiscountServices();
        return builder;
    }
}
