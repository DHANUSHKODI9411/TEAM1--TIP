using System;

namespace EFTicketPortalLibrary.Repos
{
    public class TicketException : Exception
    {
        public int StatusCode { get; }

        public TicketException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
