using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 
namespace TicketPortalWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class SLAController : ControllerBase
    {
        ISLARepository slaRepo;
 
        public SLAController(ISLARepository slaRepository)
        {
            slaRepo = slaRepository;
        }
 
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var slas = await slaRepo.GetAllSLAsAsync();
            return Ok(slas);
        }
 
        [HttpGet("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string slaId)
        {
            try
            {
                var sla = await slaRepo.GetSLAAsync(slaId);
                return Ok(sla);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
 
        [HttpGet("bytickettype/{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByTicketType(string ticketTypeId)
        {
            try
            {
                List<SLA> SLAs = await slaRepo.GetSLAsByTicketTypeIdAsync(ticketTypeId);
                return Ok(SLAs);
            }
            catch(TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(SLA sla)
        {
            try
            {
                await slaRepo.AddSLAAsync(sla);
                return Created($"api/sla/{sla.SLAid}", sla);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpPut("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string slaId, SLA sla)
        {
            try
            {
                await slaRepo.UpdateSLAAsync(slaId, sla);
                return Ok(sla);
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 502)
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }
 
        [HttpDelete("{slaId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string slaId)
        {
            try
            {
                await slaRepo.DeleteSLAAsync(slaId);
                return Ok();
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 502)
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }
    }
}
 
 