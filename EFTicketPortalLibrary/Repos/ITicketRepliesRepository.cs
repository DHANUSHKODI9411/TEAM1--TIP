using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface ITicketRepliesRepository
{
    Task AddTicketReplyAsync(TicketReplies reply);
    Task UpdateTicketReplyAsync(string replyId, TicketReplies reply);
    Task DeleteTicketReplyAsync(string replyId);
    
    Task<List<TicketReplies>> GetAllTicketRepliesAsync();
    Task<TicketReplies> GetTicketReplyAsync(string replyId);
    Task<List<TicketReplies>> GetRepliesByTicketAsync(string ticketId);
    Task<List<TicketReplies>> GetRepliesByCreatedEmployeeIdAsync(string employeeId);
    Task<List<TicketReplies>> GetRepliesByAssignedEmployeeIdAsync(string assignedEmployeeId);
}