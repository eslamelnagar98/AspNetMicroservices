namespace Discount.API.Repositories;
public interface IDiscountRepository
{
    Task<IEnumerable<Coupon>> GetAllDiscounts();
    Task<Coupon> GetDiscount(string productName);
    Task<bool> CreateDiscount(Coupon coupon);
    Task<bool> UpdateDiscount(Coupon coupon);
    Task<bool> DeleteDiscount(string productName);
}
