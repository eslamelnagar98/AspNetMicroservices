namespace Catalog.API.Data;
public class CatalogContext : ICatalogContext
{
    private readonly DatabaseSettingsOptions _options;

    private readonly ILogger<CatalogContext> _logger;
    public IMongoCollection<Product> Products { get; }
    public CatalogContext(IOptions<DatabaseSettingsOptions> options, ILogger<CatalogContext> logger)
    {
        _options = options.Value;
        _logger = logger;
        _logger.LogInformation("Connection String Value Is :{ConnectionString}", _options.ConnectionString);
        var client = new MongoClient(_options.ConnectionString);
        var database = client.GetDatabase(_options.DatabaseName);
        Products = database.GetCollection<Product>(_options.CollectionName);
        CatalogContextSeed.SeedData(Products);
    }
}
