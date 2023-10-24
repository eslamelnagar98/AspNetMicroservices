namespace Catalog.API.Repositories;
public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;
    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = Guard.Against.Null(catalogContext, nameof(catalogContext));
    }
    public async Task CreateProduct(Product product)
    {
        await _catalogContext
            .Products
            .InsertOneAsync(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var deleteResult = await _catalogContext
            .Products
            .DeleteOneAsync(GenerateFilterDefination(p => p.Id, id));

        return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _catalogContext
            .Products
            .Find(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
    {
        return await _catalogContext
        .Products
           .Find(GenerateFilterDefination(p => p.Category, categoryName))
           .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string name)
    {
        return await _catalogContext
           .Products
           .Find(GenerateFilterDefination(p => p.Name, name))
           .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _catalogContext
            .Products
            .Find(p => true)
            .ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _catalogContext
            .Products
            .ReplaceOneAsync(p => p.Id == product.Id, product);
        return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
    }

    private FilterDefinition<Product> GenerateFilterDefination(Expression<Func<Product, string>> expression, string productValue)
    {
        return Builders<Product>.Filter.Eq(expression, productValue);
    }
}
