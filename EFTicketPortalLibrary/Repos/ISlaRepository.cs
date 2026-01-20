using EFTicketPortalLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFTicketPortalLibrary.Repos
{
    public interface ISLARepository
    {
        Task AddSLAAsync(SLA sla);                         
        Task UpdateSLAAsync(string slaId, SLA sla);
        Task DeleteSLAAsync(string slaId);           
        Task<List<SLA>> GetAllSLAsAsync();                   
        Task<SLA> GetSLAAsync(string slaId);         
        Task<List<SLA>> GetSLAsByTicketTypeIdAsync(string ticketTypeId);
    }
}
            