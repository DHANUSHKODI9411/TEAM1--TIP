using EFTicketPortalLibrary.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFTicketPortalLibrary.Repos;

public class TicketRepliesRepository : ITicketRepliesRepository
{
    TicketPortalDbContext context = new TicketPortalDbContext();

    public async Task AddTicketReplyAsync(TicketReplies reply)
    {
        try
        {
            await context.TicketReplies.AddAsync(reply);
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            int errorNumber = sqlException.Number;
            switch(errorNumber)
            {
                case 2627: throw new TicketException("Reply ID already exists", 501);
                default: throw new TicketException(sqlException.Message, 599);
            }
        }
    }

    public async Task DeleteTicketReplyAsync(string replyId)
    {
        TicketReplies reply2del = await context.TicketReplies
            .FirstOrDefaultAsync(r => r.ReplyId == replyId);
        
        if(reply2del == null)
            throw new TicketException("No such reply ID", 502);
        
        context.TicketReplies.Remove(reply2del);
        await context.SaveChangesAsync();
    }

    public async Task<List<TicketReplies>> GetAllTicketRepliesAsync()
    {
        List<TicketReplies> replies = await context.TicketReplies
            .Include("Ticket")
            .Include("CreatedEmployee")
            .Include("AssignedEmployee")
            .ToListAsync();
        return replies;
    }

    public async Task<TicketReplies> GetTicketReplyAsync(string replyId)
    {
        try
        {
            TicketReplies reply = await (from r in context.TicketReplies 
                                       where r.ReplyId == replyId 
                                       select r).FirstAsync();
            return reply;
        }
        catch
        {
            throw new TicketException("No such reply ID", 502);
        }
    }

    public async Task<List<TicketReplies>> GetRepliesByTicketAsync(string ticketId)
    {
        List<TicketReplies> replies = await (from r in context.TicketReplies 
                                           where r.TicketId == ticketId 
                                           select r).ToListAsync();
        if (replies.Count == 0)
            throw new TicketException("No replies for this ticket", 504);
        return replies;
    }

    public async Task<List<TicketReplies>> GetRepliesByCreatedEmployeeIdAsync(string employeeId)
    {
        List<TicketReplies> replies = await (from r in context.TicketReplies 
                                           where r.CreatedEmployeeId == employeeId 
                                           select r).ToListAsync();
        return replies;
    }

    public async Task<List<TicketReplies>> GetRepliesByAssignedEmployeeIdAsync(string assignedEmployeeId)
    {
        List<TicketReplies> replies = await (from r in context.TicketReplies 
                                           where r.AssignedEmployeeId == assignedEmployeeId 
                                           select r).ToListAsync();
        return replies;
    }

    public async Task UpdateTicketReplyAsync(string replyId, TicketReplies reply)
    {
        TicketReplies reply2edit = await GetTicketReplyAsync(replyId);
        try
        {
            reply2edit.TicketId = reply.TicketId;
            reply2edit.CreatedEmployeeId = reply.CreatedEmployeeId;
            reply2edit.AssignedEmployeeId = reply.AssignedEmployeeId;
            reply2edit.ReplyText = reply.ReplyText;
            reply2edit.RepliedDate = reply.RepliedDate;
            await context.SaveChangesAsync();
        }
        catch(DbUpdateException ex)
        {
            SqlException sqlException = ex.InnerException as SqlException;
            throw new TicketException(sqlException.Message, 599);
        }
    }
}