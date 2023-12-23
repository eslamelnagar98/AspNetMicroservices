namespace Discount.API.Configurations;
public static partial class Extension
{
    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        app.UseSwagger()
           .UseSwaggerUI()
           .UseAuthorization();
        app.MapControllers();
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
