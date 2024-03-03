using Microsoft.Extensions.Logging;

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
           .AddGrpcClientConnection()
           .AddControllerConfigurations()
           .AddScoped<IBasketRepository, BasketRepository>()
           .AddScoped<DiscountGrpcService>();
        return services;
    }

    public static IServiceCollection AddBasketOptions<TOptions>(this IServiceCollection services, string sectionName)
        where TOptions : class
    {
        services
            .AddOptions<TOptions>()
            .Configure<IConfiguration>((options, configuration) => configuration.GetSection(sectionName).Bind(options))
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
            var configuration = new ConfigurationOptions { EndPoints = { $"{option.Host}:{option.Port}" } };
            return ConnectionMultiplexer.Connect(configuration);
        });
    }

    private static IServiceCollection AddGrpcClientConnection(this IServiceCollection services)
    {
        services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>((serviceProvider, options) =>
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                var gRPCOptions = serviceProvider.GetRequiredService<IOptions<GrpcSettingsOptions>>()?.Value;
                logger.LogInformation($"gRPC Options Discount Url Value Is {gRPCOptions.DiscountUrl}");
                options.Address = new Uri(gRPCOptions.DiscountUrl);
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Exception Happened While Trying To Handle AddGrpcClientConnection Method");
                throw;
            }
        });
        return services;
    }
}
