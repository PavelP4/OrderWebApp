﻿namespace OrderWebApp.Exceptions
{
    public class UnknownException : Exception
    {
        public UnknownException() { }

        public UnknownException(string message) : base(message) { }
    }
}
