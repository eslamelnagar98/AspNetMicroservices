namespace Discount.API.Repositories;
public class DiscountRepository : IDiscountRepository
{
    private readonly DatabaseSettingsOptions _options;
    public DiscountRepository(IOptions<DatabaseSettingsOptions> options)
    {
        ArgumentNullException.ThrowIfNull(nameof(options));
        _options = options.Value;
    }

    public async Task<IEnumerable<Coupon>> GetAllDiscounts()
    {
        using var connection = new NpgsqlConnection(_options.ConnectionString);
        var query = "SELECT * FROM Coupon";
        var coupons = await connection.QueryAsync<Coupon>(query);
        return coupons?.Any() ?? false
            ? coupons
            : Enumerable.Empty<Coupon>();
    }

    public async Task<Coupon> GetDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_options.ConnectionString);
        var query = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(query, new { ProductName = productName });
        return coupon is null ? Coupon.NullCoupon : coupon;
    }
    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_options.ConnectionString);
        var command = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)";
        var affected = await connection.ExecuteAsync(command, coupon);
        return affected is not 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_options.ConnectionString);
        var updateCommand = "UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";
        var affected = await connection.ExecuteAsync(updateCommand, coupon);
        return affected is not 0;
    }

    public async Task<bool> DeleteDiscount(string productName)
    {
        using var connection = new NpgsqlConnection(_options.ConnectionString);
        var deleteCommand = "DELETE FROM Coupon WHERE ProductName = @ProductName";
        var affected = await connection.ExecuteAsync(deleteCommand, new { ProductName = productName });
        return affected is not 0;
    }


}
