namespace OrderWebApp.Extentions
{
    public static class ConfigurationExts
    {
        public static string GetDatabaseConnection(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(Constants.Db.ConnectionName);
        }
    }
}
