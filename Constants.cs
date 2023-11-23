namespace OrderWebApp;

public static class Constants
{
    public static class Db
    {
        public const string ConnectionName = "DefaultConnection";

        public static class Store
        {
            public static Guid StoreAId => new Guid("8A54EB3C-02BC-4EFF-897F-23975F6B7647");
            public static Guid StoreBId => new Guid("691407E7-8B4C-487C-91A7-7A0DE4F43539");
            public static Guid StoreCId => new Guid("5AB26721-CBE0-4BF8-B844-1ACC696DC40D");
        }
    }
}
