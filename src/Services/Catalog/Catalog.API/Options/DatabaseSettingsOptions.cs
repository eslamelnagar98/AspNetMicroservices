namespace Catalog.API.Options;
public class DatabaseSettingsOptions
{
    public const string SectionName = "DatabaseSettings";
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }
}
