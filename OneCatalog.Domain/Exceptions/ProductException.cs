using System;

namespace OneCatalog.Domain.Exceptions
{
    public class ProductException : Exception
    {
        public ProductException(string message) : base(message)
        {
        }
    }
}