using System;

namespace OneCatalog.Application.Exceptions
{
    public class ReportException : Exception
    {
        public ReportException(string message) : base(message)
        {
        }
    }
}