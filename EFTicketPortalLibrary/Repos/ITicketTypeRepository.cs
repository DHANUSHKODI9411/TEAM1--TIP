using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface ITicketTypeRepository
{
    Task<TicketType> CreateTicketTypeAsync(TicketType ticketType);
    Task UpdateTicketTypeAsync(string ticketTypeId, TicketType ticketType);
    Task DeleteTicketTypeAsync(string ticketTypeId); 
    Task<TicketType?> GetTicketTypeAsync(string ticketTypeId);
    Task<IEnumerable<TicketType>> GetAllTicketTypesAsync();
}