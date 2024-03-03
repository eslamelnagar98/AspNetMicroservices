namespace Basket.API.GrpcServices;
public class DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoServiceClient)
{
    private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService = discountProtoServiceClient;
    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest { ProductName = productName };
        return await _discountProtoService.GetDiscountAsync(discountRequest);
    }
}
