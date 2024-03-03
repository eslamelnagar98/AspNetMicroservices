namespace Discount.Grpc.Configurations;
public static class Registration
{
    public static async Task RunDiscountAsync(this WebApplicationBuilder builder)
    {
        var logger = default(Logger);
        var serviceName = string.Empty;
        var app = default(WebApplication);
        try
        {
            logger = Extension.GetLogger();
            serviceName = Extension.GetServiceName();
            app = builder.Build();
            await app.MigrateDatabaseAsync();
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

    public static IServiceCollection AddDiscountServices(this IServiceCollection services)
    {
        services
            .AddScoped<IDiscountRepository, DiscountRepository>()
            .AddGrpc();
        return services;
    }

    public static IServiceCollection AddDiscountOptions(this IServiceCollection services)
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

    private static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        var logger = default(ILogger<Program>);
        try
        {
            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            logger = services.GetRequiredService<ILogger<Program>>();
            var discountRepository = services.GetRequiredService<IDiscountRepository>();
            logger.LogInformation("Migrating postresql database.");
            var coupons = await discountRepository.GetAllDiscounts();
            if (coupons.Count() is 0)
            {
                await discountRepository.SeedDataAsync();
            }
            logger.LogInformation("Migrated postresql Database Done Successfully.");
        }
        catch (NpgsqlException ex)
        {
            logger.LogError(ex, "An error occurred while migrating the postresql database");
            throw;
        }
    }

    private static async Task SeedDataAsync(this IDiscountRepository discountRepository)
    {
        await discountRepository.CreateDiscount(new Coupon
        { ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 });
        await discountRepository.CreateDiscount(new Coupon
        { ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 150 });
    }

}