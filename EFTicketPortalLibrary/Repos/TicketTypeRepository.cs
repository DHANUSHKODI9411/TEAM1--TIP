using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using EFTicketPortalLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFTicketPortalLibrary.Repos;

public class TicketTypeRepository : ITicketTypeRepository
{
    TicketPortalDbContext context = new TicketPortalDbContext();

    public async Task<TicketType> CreateTicketTypeAsync(TicketType ticketType)
    {
        try
        {
            await context.TicketTypes.AddAsync(ticketType);
            await context.SaveChangesAsync();
            return ticketType;
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;
            switch (errorNumber)
            {
                case 2627: 
                    throw new TicketException("Ticket Type ID already exists", 501);
                default:
                    throw new TicketException(sqlException.Message, 599);
            }
        }
    }

    public async Task UpdateTicketTypeAsync(string ticketTypeId, TicketType ticketType)
    {
        TicketType typeToEdit = await GetTicketTypeAsync(ticketTypeId); 
        try
        {
            typeToEdit.TicketTypeName = ticketType.TicketTypeName;  
            typeToEdit.Description = ticketType.Description;   
            await context.SaveChangesAsync(); 
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new TicketException(sqlException.Message, 599); 
        }
    }
    public async Task<IEnumerable<TicketType>> GetAllTicketTypesAsync()
    {
        return await context.TicketTypes.ToListAsync();
    }
    public async Task DeleteTicketTypeAsync(string ticketTypeId)
    {
        TicketType typeToDelete = await context.TicketTypes
            .Include("Tickets")
            .Include("Slas")
            .FirstOrDefaultAsync(t => t.TicketTypeId == ticketTypeId);

        if (typeToDelete == null)
            throw new TicketException("No such ticket type ID", 502);

        if (typeToDelete.Tickets.Count == 0 && typeToDelete.Slas.Count == 0)
        {
            context.TicketTypes.Remove(typeToDelete);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new TicketException("Cannot delete because this type is linked to existing Tickets or SLAs", 603);
        }
    }

    public async Task<TicketType?> GetTicketTypeAsync(string ticketTypeId)
    {
        try
        {
            TicketType ticketType = await (from t in context.TicketTypes where t.TicketTypeId == ticketTypeId select t).FirstAsync();
            return ticketType;
        }
        catch
        {
            throw new TicketException("No such ticket type ID", 502);
        }
    }
}