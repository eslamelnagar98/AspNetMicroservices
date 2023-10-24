using Catalog.API.Data.SeedData;

namespace Catalog.API.Data;
public class CatalogContext : ICatalogContext
{
    private readonly DatabaseSettingsOptions _options;
    public IMongoCollection<Product> Products { get; }
    public CatalogContext(IOptions<DatabaseSettingsOptions> options)
    {
        _options = options.Value;
        var client = new MongoClient(_options.ConnectionString);
        var database = client.GetDatabase(_options.DatabaseName);
        Products = database.GetCollection<Product>(_options.CollectionName);
        CatalogContextSeed.SeedData(Products);
    }
}
