namespace Basket.API.Repositories;
public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;
    public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _database = connectionMultiplexer.GetDatabase();
    }
    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basket = await _database.StringGetAsync(userName);
        return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        var basketToBeUpdated = JsonSerializer.Serialize(basket);
        var created = await _database.StringSetAsync(basket.UserName, basketToBeUpdated, TimeSpan.FromDays(30));
        return created ? await GetBasket(basket.UserName) : null;
    }

    public async Task DeleteBasket(string userName)
        => await _database.KeyDeleteAsync(userName);

}
