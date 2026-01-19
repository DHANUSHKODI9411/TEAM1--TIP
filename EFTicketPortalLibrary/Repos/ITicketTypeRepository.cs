using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface ITicketTypeRepository
{
    Task<TicketType> CreateTicketTypeAsync(TicketType ticketType);
    Task<TicketType> UpdateTicketTypeAsync(TicketType ticketType); 
    Task DeleteTicketTypeAsync(string ticketTypeId); 
    Task<TicketType?> GetTicketTypeByIdAsync(string ticketTypeId);
    Task<IEnumerable<TicketType>> GetAllTicketTypesAsync();
}