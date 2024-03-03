namespace Discount.Grpc.Entities;
public class Coupon
{
    public int Id { get; init; }
    public string ProductName { get; init; }
    public string Description { get; init; }
    public int Amount { get; init; }
    public static Coupon NullCoupon { get; } = new() { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

    public static explicit operator CouponModel(Coupon coupon)
    {
        return new()
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Amount = coupon.Amount,
            Description = coupon.Description,
        };
    }

    public static explicit operator Coupon(CouponModel coupon)
    {
        return new()
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Amount = coupon.Amount,
            Description = coupon.Description,
        };
    }
}
