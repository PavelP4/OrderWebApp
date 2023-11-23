namespace OrderWebApp;

public static class Constants
{
    public static class EnvironmentVariableNames
    {
        public const string DatabaseConnectionString = "ASPNETCORE_DATABASE_CONNECTION_STRING";
    }

    public static class Db
    {
        public static class Store
        {
            public static Guid StoreAId => new("8A54EB3C-02BC-4EFF-897F-23975F6B7647");
            public static Guid StoreBId => new("691407E7-8B4C-487C-91A7-7A0DE4F43539");
            public static Guid StoreCId => new("5AB26721-CBE0-4BF8-B844-1ACC696DC40D");
        }
    }
}
