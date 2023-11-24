namespace OrderWebApp.Exceptions
{
    public class AppException : ApplicationException
    {
        public AppException() { }

        public AppException(string message) : base(message) { }
    }
}
