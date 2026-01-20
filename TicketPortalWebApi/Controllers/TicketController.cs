using Microsoft.AspNetCore.Http;
using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketPortalWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : ControllerBase
    {
        ITicketRepository _ticketRepository;
        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            return Ok(tickets);
        }
        [HttpGet("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketAsync(ticketId);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
 
        [HttpGet("bycreatedemployee/{createdEmployeeId}")]
        public async Task<ActionResult> GetByCreatedEmployee(string createdEmployeeId)
        {
            var tickets = await _ticketRepository.GetByCreatedEmployeeIdAsync(createdEmployeeId);
            return Ok(tickets);
        }
 
        [HttpGet("byassignedemployee/{assignedEmployeeId}")]
        public async Task<ActionResult> GetByAssignedEmployee(string assignedEmployeeId)
        {
            var tickets = await _ticketRepository.GetByAssignedEmployeeIdAsync(assignedEmployeeId);
            return Ok(tickets);
        }
 
        [HttpGet("bystatus/{statusId}")]
        public async Task<ActionResult> GetByStatus(string statusId)
        {
            var tickets = await _ticketRepository.GetByStatusIdAsync(statusId);
            return Ok(tickets);
        }
        [HttpGet("bytickettype/{ticketTypeId}")]
        public async Task<ActionResult> GetByTicketType(string ticketTypeId)
        {
            var tickets = await _ticketRepository.GetByTicketTypeIdAsync(ticketTypeId);
            return Ok(tickets);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(Ticket ticket)
        {
            try
            {
                await _ticketRepository.CreateTicketAsync(ticket);
                return Created($"api/ticket/{ticket.TicketId}", ticket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpPut("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string ticketId, Ticket ticket)
        {
            try
            {
                await _ticketRepository.UpdateTicketAsync(ticketId, ticket);
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }
 
        [HttpDelete("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string ticketId)
        {
            try
            {
                await _ticketRepository.DeleteTicketAsync(ticketId);
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

 