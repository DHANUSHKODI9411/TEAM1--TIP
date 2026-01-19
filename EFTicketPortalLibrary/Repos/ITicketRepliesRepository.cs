using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface ITicketRepliesRepository
{
    Task<List<TicketReplies>> GetAllTicketRepliesAsync();
    Task<TicketReplies> GetTicketReplyAsync(string replyId);
    Task<List<TicketReplies>> GetRepliesByTicketAsync(string ticketId);

    Task AddTicketReplyAsync(TicketReplies reply);
    Task UpdateTicketReplyAsync(string replyId, TicketReplies reply);
    Task DeleteTicketReplyAsync(string replyId);
}
