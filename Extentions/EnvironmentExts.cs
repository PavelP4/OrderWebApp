namespace OrderWebApp.Extentions
{
    public static class EnvironmentExts
    {
        public static string GetDbConnectionString(this IHostEnvironment environment)
        {
            return Environment.GetEnvironmentVariable(Constants.EnvironmentVariableNames.DatabaseConnectionString);
        }
    }
}
