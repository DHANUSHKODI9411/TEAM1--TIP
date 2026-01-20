using EFTicketPortalLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFTicketPortalLibrary.Repos;

public class SLARepository: ISLARepository
{
    TicketPortalDbContext context = new TicketPortalDbContext();

    public async Task AddSLAAsync(SLA sla)
    {
        try
        {
            await context.SLAs.AddAsync(sla);
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;
            switch(errorNumber)
            {
                case 2627: 
                case 2601: throw new SLAException("SLA ID or TicketType ID already exists", 501);
                default: throw new SLAException(sqlException.Message, 599);
            }
        }
    }

    public async Task DeleteSLAAsync(string slaId)
    {
        SLA sla2del = await context.SLAs.Include("TicketType").ThenInclude("Tickets")
            .FirstOrDefaultAsync(sla => sla.SLAid == slaId);
        
        if(sla2del == null)
            throw new SLAException("No such SLA ID", 502);
        if (sla2del.TicketType.Tickets.Count == 0)
        {
            context.SLAs.Remove(sla2del);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new SLAException("Cannot delete SLA linked to ticket type with active tickets", 503);
        }
    }

    public async Task<List<SLA>> GetAllSLAsAsync()
    {
        List<SLA> slas = await context.SLAs.Include("TicketType").ToListAsync();
        return slas;
    }

    public async Task<SLA> GetSLAAsync(string slaId)
    {
        try
        {
            SLA sla = await (from p in context.SLAs where p.SLAid == slaId select p).FirstAsync();
            return sla;
        }
        catch
        {
            throw new SLAException("No such SLA ID", 502);
        }
    }

    public async Task<List<SLA>> GetSLAsByTicketTypeIdAsync(string ticketTypeId)
    {
        List<SLA> slas = await (from p in context.SLAs where p.TicketTypeId == ticketTypeId select p).ToListAsync();
        if (slas.Count == 0)
            throw new SLAException("No SLAs for this ticket type", 504);
        return slas;
    }

    public async Task UpdateSLAAsync(string slaId, SLA sla)
    {
        SLA sla2edit = await GetSLAAsync(slaId);
        try
        {
            sla2edit.TicketTypeId = sla.TicketTypeId;
            sla2edit.ResponseTime = sla.ResponseTime;
            sla2edit.ResolutionTime = sla.ResolutionTime;
            sla2edit.Description = sla.Description;
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new SLAException(sqlException.Message, 599);
        }
    }
}
