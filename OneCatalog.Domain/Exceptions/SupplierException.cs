using System;

namespace OneCatalog.Domain.Exceptions
{
    public class SupplierException : Exception
    {
        public SupplierException(string message) : base(message)
        {
        }
    }
}