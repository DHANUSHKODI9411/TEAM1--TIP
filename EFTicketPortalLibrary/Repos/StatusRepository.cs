using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public class StatusRepository : IStatusRepository
{
    EYInstituteDBContext context = new EYInstituteDBContext();
    public async Task AddStatusAsync(Status status)
    {
        try
        {
            await context.Statuses.AddAsync(status);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;
            switch (errorNumber)
            {
                case 2627:
                case 2601:  // Unique constraint violation for StatusId or StatusName
                    throw new TicketException("Status ID or Name already exists", 501);
                default:
                    throw new TicketException(sqlException.Message, 599);
            }
        }
    }

    public async Task DeleteStatusAsync(string statusId)
    {
        Status status2del = await context.Statuses.Include("Tickets").FirstOrDefaultAsync(s => s.StatusId == statusId);

        if (status2del == null)
            throw new TicketException("No such status ID", 502);

        if (status2del.Tickets.Count == 0)
        {
            context.Statuses.Remove(status2del);
            await context.SaveChangesAsync();
        }
        else
        {
            throw new TicketException("Cannot delete status used in active tickets", 503);
        }
    }

    public async Task<List<Status>> GetAllStatusesAsync()
    {
        List<Statuses> status = await context.Statuses.ToListAsync();
        return status;
    }

    public async Task<Status> GetStatusAsync(string statusId)
    {
        try
        {
            Status status = await context.Statuses
                .FirstAsync(s => s.StatusId == statusId);
            return status;
        }
        catch (InvalidOperationException)
        {
            throw new TicketException("No such status ID", 502);
        }
    }

    public async Task UpdateStatusAsync(string statusId, Status status)
    {
        Status status2edit = await GetStatusAsync(statusId);
        try
        {
            status2edit.StatusName = status.StatusName;
            status2edit.Description = status.Description;
            status2edit.IsActive = status.IsActive;
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new TicketException(sqlException.Message, 599);
        }
    }
}