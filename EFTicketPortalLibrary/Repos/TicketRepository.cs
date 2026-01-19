using System;

using EFTicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTicketPortalLibrary.Repos;

public class TicketRepository : ITicketRepository
{
    private readonly TicketPortalDbContext _context = new();

    public async Task CreateTicketAsync(Ticket ticket)
    {
        try
        {
            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new TicketException(
                $"Unexpected error while creating ticket. {ex.Message}", 500);
        }
    }

    public async Task UpdateTicketAsync(Ticket ticket)
    {
        var existing = await GetTicketByIdAsync(ticket.TicketId);

        try
        {
            existing.Title = ticket.Title;
            existing.Description = ticket.Description;
            existing.StatusId = ticket.StatusId;
            existing.AssignedEmployeeId = ticket.AssignedEmployeeId;
            existing.TicketTypeId = ticket.TicketTypeId;

            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new TicketException(
                $"Unexpected error while updating ticket. {ex.Message}", 500);
        }
    }

    public async Task DeleteTicketAsync(string ticketId)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);

        if (ticket == null)
        {
            throw new TicketException("Ticket not found.", 404);
        }

        _context.Tickets.Remove(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task<Ticket> GetTicketByIdAsync(string ticketId)
    {
        var ticket = await _context.Tickets
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);

        if (ticket == null)
        {
            throw new TicketException("Ticket not found.", 404);
        }

        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets.ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByEmployeeIdAsync(string employeeId)
    {
        return await _context.Tickets
            .Where(t => t.EmployeeId == employeeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByAssignedEmployeeIdAsync(string assignedEmployeeId)
    {
        return await _context.Tickets
            .Where(t => t.AssignedEmployeeId == assignedEmployeeId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByStatusIdAsync(string statusId)
    {
        return await _context.Tickets
            .Where(t => t.StatusId == statusId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(string ticketTypeId)
    {
        return await _context.Tickets
            .Where(t => t.TicketTypeId == ticketTypeId)
            .ToListAsync();
    }
}

