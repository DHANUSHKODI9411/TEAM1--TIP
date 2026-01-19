using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface IStatusRepository
{
    Task AddStatusAsync(Status status);
    Task UpdateStatusAsync(string StatusId, Status status);
    Task DeleteStatusAsync(string StatusId);
    Task<List<Status>> GetAllStatussAsync();
    Task<Status> GetStatusAsync(string StatusId);
}
