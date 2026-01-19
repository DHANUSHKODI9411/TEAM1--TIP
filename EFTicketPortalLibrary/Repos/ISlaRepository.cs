using EFTicketPortalLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFTicketPortalLibrary.Repositories
{
    public interface ISLARepository
    {
        Task<IEnumerable<SLA>> GetAllAsync();
        Task<SLA?> GetByIdAsync(string slaId);
        Task<SLA?> GetByTicketTypeIdAsync(string ticketTypeId);
        Task AddAsync(SLA sla);
        Task UpdateAsync(SLA sla);
        Task DeleteAsync(string slaId);
    }
}
