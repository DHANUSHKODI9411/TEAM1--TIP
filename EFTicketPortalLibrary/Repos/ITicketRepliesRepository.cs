using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface TicketRepliesRepository
{
    Task<IEnumerable<TicketReplies>> GetAllRepliesAsync();

        Task<TicketReplies?> GetReplyByIdAsync(string replyId);

        Task<TicketReplies> GetRepliesByTicketIdAsync(string ticketId);

        Task AddReplyAsync(TicketReplies reply);

        Task UpdateReplyAsync(TicketReplies reply);

        Task DeleteReplyAsync(string replyId);

        Task SaveAsync();
    
}
