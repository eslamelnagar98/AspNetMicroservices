namespace Discount.API.Options;
public class DatabaseSettingsOptions
{
    public const string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; }
}
