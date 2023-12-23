namespace Discount.API.Entities;
public class Coupon
{
    public int Id { get; init; }
    public string ProductName { get; init; }
    public string Description { get; init; }
    public int Amount { get; init; }
    public static Coupon NullCoupon { get; }= new() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
}
