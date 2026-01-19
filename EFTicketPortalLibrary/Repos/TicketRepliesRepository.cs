using System;
using EFTicketPortalLibrary.Models;
namespace EFTicketPortalLibrary.Repos;

public class TicketRepliesRepository : ITicketRepliesRepository
{
    EYTicketPortalDBContext context = new EYTicketPortalDBContext();  // Replace with your actual DbContext
 
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
                case 2627:
                case 2601: throw new TicketException("Reply ID or foreign key constraint violation", 501);
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
 
    public async Task<List<TicketReplies>> GetRepliesByEmployeeAsync(string employeeId)
    {
        List<TicketReplies> replies = await (from r in context.TicketReplies 
                                           where r.CreatedEmployeeId == employeeId || r.AssignedEmployeeId == employeeId
                                           select r).ToListAsync();
        return replies;
    }
 
    public async Task UpdateTicketReplyAsync(string replyId, TicketReplies reply)
    {
        TicketReplies reply2edit = await GetTicketReplyAsync(replyId);
        try
        {
            reply2edit.ReplyText = reply.ReplyText;
            reply2edit.CreatedEmployeeId = reply.CreatedEmployeeId;
            reply2edit.AssignedEmployeeId = reply.AssignedEmployeeId;
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
