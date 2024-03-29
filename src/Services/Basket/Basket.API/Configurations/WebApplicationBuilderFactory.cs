﻿namespace Basket.API.Configurations;
public static class WebApplicationBuilderFactory
{
    public static WebApplicationBuilder CreateBasketWebBuilder(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseNLog();
        builder
            .Services
            .AddBasketOptions<RedisConnectionStringOptions>(RedisConnectionStringOptions.SectionName)
            .AddBasketOptions<GrpcSettingsOptions>(GrpcSettingsOptions.SectionName)
            .AddBasketServices();
        return builder;
    }
}
