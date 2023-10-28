namespace Basket.API.Configurations;
public static class Registration
{
    public static async Task RunBasketAsync(this WebApplicationBuilder builder)
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
    public static IServiceCollection AddBasketServices(this IServiceCollection services)
    {
        services
           .AddRedisConnection()
           .AddScoped<IBasketRepository,BasketRepository>()
           .AddControllerConfigurations();
        return services;
    }

    public static IServiceCollection AddCatalogOptions(this IServiceCollection services)
    {
        services
            .AddOptions<RedisConnectionStringOptions>()
            .Configure<IConfiguration>(
            (options, configuration) =>
             configuration.GetSection(RedisConnectionStringOptions.SectionName)
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

    private static IServiceCollection AddRedisConnection(this IServiceCollection services)
    {
        return services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
        {
            var option = serviceProvider.GetRequiredService<IOptions<RedisConnectionStringOptions>>()?.Value;
            //var configuration = $"{option.Host}:{option.Port}";
            var configuration = new ConfigurationOptions { EndPoints = { $"{option.Host}:{option.Port}" } };
            return ConnectionMultiplexer.Connect(configuration);
        });
    }
}
