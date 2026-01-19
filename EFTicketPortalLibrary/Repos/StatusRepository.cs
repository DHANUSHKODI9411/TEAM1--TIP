using System;
using EFTicketPortalLibrary.Models;

namespace EFTicketPortalLibrary.Repos;

public class StatusRepository : IStatusRepository
{
    EYInstituteDBContext context = new EYInstituteDBContext();
    public async Task AddStatusAsync(Status status)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteStatusAsync(string StatusId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Status>> GetAllStatussAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Status> GetStatusAsync(string StatusId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateStatusAsync(string StatusId, Status status)
    {
        throw new NotImplementedException();
    }
}