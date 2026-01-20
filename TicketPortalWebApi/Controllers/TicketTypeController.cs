using Microsoft.AspNetCore.Http;
using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
namespace TicketPortalWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketTypeController : ControllerBase
    {
        ITicketTypeRepository _ticketTypeRepository;
 
        public TicketTypeController(ITicketTypeRepository ticketTypeRepository)
        {
            _ticketTypeRepository = ticketTypeRepository;
        }
 
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ticketTypes = await _ticketTypeRepository.GetAllTicketTypesAsync();
            return Ok(ticketTypes);
        }
 
        [HttpGet("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string ticketTypeId)
        {
            try
            {
                var ticketType = await _ticketTypeRepository.GetTicketTypeAsync(ticketTypeId);
 
                if (ticketType == null)
                    return NotFound("Ticket Type not found");
 
                return Ok(ticketType);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(TicketType ticketType)
        {
            try
            {
                var createdTicketType = await _ticketTypeRepository.CreateTicketTypeAsync(ticketType);
                return Created($"api/tickettype/{createdTicketType.TicketTypeId}", createdTicketType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpPut("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string ticketTypeId, TicketType ticketType)
        {
            try
            {
                await _ticketTypeRepository.UpdateTicketTypeAsync(ticketTypeId, ticketType);
                return Ok(ticketType);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }
 
        [HttpDelete("{ticketTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string ticketTypeId)
        {
            try
            {
                await _ticketTypeRepository.DeleteTicketTypeAsync(ticketTypeId);
                return Ok();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }
    }
}
 
 