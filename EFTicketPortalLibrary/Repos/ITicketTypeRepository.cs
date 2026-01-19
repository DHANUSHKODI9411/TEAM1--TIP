using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface ITicketTypeRepository
{
    Task<TicketType> CreateTicketTypeAsync(TicketType ticketType);
    Task<TicketType> UpdateTicketAsync(TicketType ticketType);
    Task DeleteTicketTypeAsync(int ticketTypeId);
    Task<TicketType?>GetTicketTypeByIdAsync(int ticketTypeId);
    Task<IEnumerable<TicketType>> GetAllTicketTypesAsync();
}
