using System;

namespace EFTicketPortalLibrary.Models;

public class TicketException: Exception
{
    public int ErrorNumber { get; set; }
    public TicketException(string errMsg, int errNo) : base(errMsg)
    {
        ErrorNumber = errNo;
    }
}
