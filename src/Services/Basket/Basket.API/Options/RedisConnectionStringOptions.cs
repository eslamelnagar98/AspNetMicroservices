namespace Basket.API.Options;
public class RedisConnectionStringOptions
{
    public const string SectionName = "RedisConnectionString";
    public string Host { get; set; }
    public short Port { get; set; }
}
