namespace Basket.API.Entities;
public class ShoppingCart
{
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice => GetTotalItemsPrice();
    public ShoppingCart() { }
    public ShoppingCart(string username)
    {
        UserName = username;
    }

    private decimal GetTotalItemsPrice()
    {
        return Items.Aggregate(default(decimal), (current, next) => current + (next.Quantity * next.Price));
    }
}
