using System;

namespace OneCatalog.Application.Exceptions
{
    public class TenantException : Exception
    {
        public TenantException(string message) : base(message)
        {
        }
    }
}