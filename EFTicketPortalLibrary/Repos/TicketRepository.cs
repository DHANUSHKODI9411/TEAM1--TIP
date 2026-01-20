using EFTicketPortalLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFTicketPortalLibrary.Repos;

public class TicketRepository : ITicketRepository
{
    TicketPortalDbContext context = new TicketPortalDbContext();

    public async Task CreateTicketAsync(Ticket ticket)
    {
        try
        {
            await context.Tickets.AddAsync(ticket);
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;
            switch(errorNumber)
            {
                case 2627: throw new TicketException("Ticket ID already exists", 501);
                default: throw new TicketException(sqlException.Message, 599);
            }
        }
    }

    public async Task DeleteTicketAsync(string ticketId)
    {
        Ticket ticket2del = await context.Tickets.Include("Replies")
            .FirstOrDefaultAsync(t => t.TicketId == ticketId);
        
        if(ticket2del == null)
            throw new TicketException("No such ticket ID", 502);
        
        if (ticket2del.Replies.Count == 0)
        {
            context.Tickets.Remove(ticket2del);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new TicketException("Cannot delete ticket with replies", 503);
        }
    }

    public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
    {
        List<Ticket> tickets = await context.Tickets
            .Include("CreatedEmployee")
            .Include("AssignedEmployee")
            .Include("TicketType")
            .Include("Status")
            .Include("Replies")
            .ToListAsync();
        return tickets;
    }

    public async Task<Ticket> GetTicketAsync(string ticketId)
    {
        try
        {
            Ticket ticket = await (from t in context.Tickets 
                                 where t.TicketId == ticketId 
                                 select t).FirstAsync();
            return ticket;
        }
        catch
        {
            throw new TicketException("No such ticket ID", 502);
        }
    }

    public async Task<IEnumerable<Ticket>> GetByCreatedEmployeeIdAsync(string createdEmployeeId)
    {
        List<Ticket> tickets = await (from t in context.Tickets 
                                    where t.CreatedEmployeeId == createdEmployeeId 
                                    select t).ToListAsync();
        if (tickets.Count == 0)
            throw new TicketException("No tickets created by this employee", 504);
        return tickets;
    }

    public async Task<IEnumerable<Ticket>> GetByAssignedEmployeeIdAsync(string assignedEmployeeId)
    {
        List<Ticket> tickets = await (from t in context.Tickets 
                                    where t.AssignedEmployeeId == assignedEmployeeId 
                                    select t).ToListAsync();
        return tickets;
    }

    public async Task<IEnumerable<Ticket>> GetByStatusIdAsync(string statusId)
    {
        List<Ticket> tickets = await (from t in context.Tickets 
                                    where t.StatusId == statusId 
                                    select t).ToListAsync();
        if (tickets.Count == 0)
            throw new TicketException("No tickets with this status", 504);
        return tickets;
    }

    public async Task<IEnumerable<Ticket>> GetByTicketTypeIdAsync(string ticketTypeId)
    {
        List<Ticket> tickets = await (from t in context.Tickets 
                                    where t.TicketTypeId == ticketTypeId 
                                    select t).ToListAsync();
        if (tickets.Count == 0)
            throw new TicketException("No tickets of this type", 504);
        return tickets;
    }

    public async Task UpdateTicketAsync(string ticketId, Ticket ticket)
    {
        Ticket ticket2edit = await GetTicketAsync(ticketId);
        try
        {
            ticket2edit.Title = ticket.Title;
            ticket2edit.Description = ticket.Description;
            ticket2edit.CreatedEmployeeId = ticket.CreatedEmployeeId;
            ticket2edit.TicketTypeId = ticket.TicketTypeId;
            ticket2edit.StatusId = ticket.StatusId;
            ticket2edit.AssignedEmployeeId = ticket.AssignedEmployeeId;
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new TicketException(sqlException.Message, 599);
        }
    }
}