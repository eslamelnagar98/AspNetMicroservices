namespace Basket.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService) : ControllerBase
{
    private readonly IBasketRepository _basketRepository = Guard.Against.Null(basketRepository);

    private readonly DiscountGrpcService _discountGrpcService = Guard.Against.Null(discountGrpcService);

    [HttpGet("{userName}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
    {
        var basket = await _basketRepository.GetBasket(userName);
        return basket is null ? NotFound($"Basket With Key: {userName} Not Found") : Ok(basket);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        basket.Items = await UpdateBasketItemsPrice(basket.Items).ToListAsync();
        var createdBasket = await _basketRepository.UpdateBasket(basket);
        return Ok(createdBasket);
    }

    [HttpDelete("{userName}", Name = "DeleteBasket")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await _basketRepository.DeleteBasket(userName);
        return Ok();
    }

    private async IAsyncEnumerable<ShoppingCartItem> UpdateBasketItemsPrice(List<ShoppingCartItem> basketItems)
    {
        foreach (var basketItem in basketItems)
        {
            var coupon = await _discountGrpcService.GetDiscount(basketItem.ProductName);
            basketItem.Price -= coupon.Amount;
            yield return basketItem;
        }
    }

}
