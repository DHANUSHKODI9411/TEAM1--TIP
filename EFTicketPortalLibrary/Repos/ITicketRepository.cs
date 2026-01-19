using System;

using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface ITicketRepository
{
    Task CreateTicketAsync(Ticket ticket);
    Task UpdateTicketAsync(Ticket ticket);

    Task DeleteTicketAsync(string ticketId);

    Task<Ticket> GetTicketByIdAsync(string ticketId);

    Task<IEnumerable<Ticket>> GetAllTicketsAsync();
    Task<IEnumerable<Ticket>> GetByCreatedEmployeeIdAsync(string createdEmployeeId);
    Task<IEnumerable<Ticket>> GetByAssignedEmployeeIdAsync(string assignedEmployeeId);
    Task<IEnumerable<Ticket>> GetByStatusIdAsync(string statusId);
    Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(string ticketTypeId);
}

