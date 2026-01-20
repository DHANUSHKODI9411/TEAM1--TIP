using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EFTicketPortalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SLAController : ControllerBase
    {
        private readonly ISLARepository _slaRepository;

        public SLAController(ISLARepository slaRepository)
        {
            _slaRepository = slaRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddSLA([FromBody] SLA sla)
        {
            try
            {
                await _slaRepository.AddSLAAsync(sla);
                return Ok("SLA added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{slaId}")]
        public async Task<IActionResult> UpdateSLA(string slaId, [FromBody] SLA sla)
        {
            try
            {
                await _slaRepository.UpdateSLAAsync(slaId, sla);
                return Ok("SLA updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{slaId}")]
        public async Task<IActionResult> DeleteSLA(string slaId)
        {
            try
            {
                await _slaRepository.DeleteSLAAsync(slaId);
                return Ok("SLA deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSLAs()
        {
            try
            {
                var slas = await _slaRepository.GetAllSLAsAsync();
                return Ok(slas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{slaId}")]
        public async Task<IActionResult> GetSLA(string slaId)
        {
            try
            {
                var sla = await _slaRepository.GetSLAAsync(slaId);
                return Ok(sla);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("by-ticket-type/{ticketTypeId}")]
        public async Task<IActionResult> GetByTicketType(string ticketTypeId)
        {
            try
            {
                var slas = await _slaRepository.GetSLAsByTicketTypeIdAsync(ticketTypeId);
                return Ok(slas);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
