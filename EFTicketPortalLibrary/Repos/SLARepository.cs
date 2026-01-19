using EFTicketPortalLibrary.Data;
using EFTicketPortalLibrary.Repositories;
using System;
using EFTicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFTicketPortalLibrary.Repositories
{
    public class SLARepository : ISLARepository
    {
        private readonly AppDbContext _context;

        public SLARepository(AppDbContext context)
        {
            _context = context;
        }

        // Get all SLAs
        public async Task<IEnumerable<SLA>> GetAllAsync()
        {
            return await _context.SLAs
                .Include(s => s.TicketType)
                .ToListAsync();
        }

        // Get SLA by SLAid
        public async Task<SLA?> GetByIdAsync(string slaId)
        {
            return await _context.SLAs
                .Include(s => s.TicketType)
                .FirstOrDefaultAsync(s => s.SLAid == slaId);
        }

        // Get SLA by TicketTypeId
        public async Task<SLA?> GetByTicketTypeIdAsync(string ticketTypeId)
        {
            return await _context.SLAs
                .FirstOrDefaultAsync(s => s.TicketTypeId == ticketTypeId);
        }

        // Add new SLA
        public async Task AddAsync(SLA sla)
        {
            _context.SLAs.Add(sla);
            await _context.SaveChangesAsync();
        }

        // Update SLA
        public async Task UpdateAsync(SLA sla)
        {
            _context.SLAs.Update(sla);
            await _context.SaveChangesAsync();
        }

        // Delete SLA
        public async Task DeleteAsync(string slaId)
        {
            var sla = await GetByIdAsync(slaId);
            if (sla != null)
            {
                _context.SLAs.Remove(sla);
                await _context.SaveChangesAsync();
            }
        }
    }
}
