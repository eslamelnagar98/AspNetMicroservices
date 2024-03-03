namespace Discount.Grpc.Configurations;
public static partial class Extension
{
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.MapGrpcService<DiscountService>();
        app.MapGet("/", () => "Communication with gRPC");
        return app;
    }

    public static Logger GetLogger()
    {
        return LogManager
            .Setup()
            .LoadConfigurationFromAppSettings()
            .GetCurrentClassLogger();
    }
    public static string GetServiceName()
    {
        return Assembly
            .GetExecutingAssembly()
            .GetName()
            .Name;
    }
}
