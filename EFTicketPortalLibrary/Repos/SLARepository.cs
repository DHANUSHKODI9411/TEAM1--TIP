using EFTicketPortalLibrary.Data;
using EFTicketPortalLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using EFTicketPortalLibrary.Repositories;

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
            try
            {
                return await _context.SLAs
                    .Include(s => s.TicketType)
                    .ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error fetching SLA list.");
            }
        }

        // Get SLA by SLAid
        public async Task<SLA?> GetByIdAsync(string slaId)
        {
            try
            {
                var sla = await _context.SLAs
                    .Include(s => s.TicketType)
                    .FirstOrDefaultAsync(s => s.SLAid == slaId);

                if (sla == null)
                    throw new Exception("SLA not found.");

                return sla;
            }
            catch (Exception)
            {
                throw new Exception("Error fetching SLA by ID.");
            }
        }

        // Get SLA by TicketTypeId
        public async Task<SLA?> GetByTicketTypeIdAsync(string ticketTypeId)
        {
            try
            {
                return await _context.SLAs
                    .FirstOrDefaultAsync(s => s.TicketTypeId == ticketTypeId);
            }
            catch (Exception)
            {
                throw new Exception("Error fetching SLA by Ticket Type.");
            }
        }

        // Add new SLA
        public async Task AddAsync(SLA sla)
        {
            try
            {
                _context.SLAs.Add(sla);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException)
            {
                throw new Exception("Database error while adding SLA.");
            }
            catch (Exception)
            {
                throw new Exception("Unexpected error while adding SLA.");
            }
        }

        // Update SLA
        public async Task UpdateAsync(SLA sla)
        {
            try
            {
                _context.SLAs.Update(sla);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException)
            {
                throw new Exception("Database error while updating SLA.");
            }
            catch (Exception)
            {
                throw new Exception("Unexpected error while updating SLA.");
            }
        }

        // Delete SLA
        public async Task DeleteAsync(string slaId)
        {
            try
            {
                var sla = await GetByIdAsync(slaId);

                if (sla == null)
                    throw new Exception("SLA not found.");

                _context.SLAs.Remove(sla);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Error deleting SLA.");
            }
        }
    }
}
