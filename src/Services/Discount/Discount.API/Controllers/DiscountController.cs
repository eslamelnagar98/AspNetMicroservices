namespace Discount.API.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController: ControllerBase
{
    private readonly IDiscountRepository _dicountRepository;
    public DiscountController(IDiscountRepository discountRepository)
    {
        _dicountRepository = discountRepository ?? throw new ArgumentNullException(nameof(discountRepository));
    }

    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> GetDiscount(string productName)
    {
        var coupon = await _dicountRepository.GetDiscount(productName);
        return Ok(coupon);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
    {
        await _dicountRepository.CreateDiscount(coupon);
        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
    {
        var updateDiscount = await _dicountRepository.UpdateDiscount(coupon);
        return Ok(updateDiscount);
    }

    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<bool>> DeleteDiscount(string productName)
    {
        var deleteDiscount = await _dicountRepository.DeleteDiscount(productName);
        return Ok(deleteDiscount);
    }
}
