using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public interface IStatusRepository
{
    Task AddStatusAsync(Status status);
    Task UpdateStatusAsync(string statusId, Status status);
    Task DeleteStatusAsync(string statusId);
    Task<List<Status>> GetAllStatusesAsync();
    Task<Status> GetStatusAsync(string statusId);
}
