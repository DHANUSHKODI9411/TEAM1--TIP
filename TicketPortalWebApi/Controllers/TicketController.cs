using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{ticketId}")]
        public async Task<ActionResult> GetTicket(string ticketId)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketAsync(ticketId);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("created/{employeeId}")]
        public async Task<ActionResult> GetByCreatedEmployee(string employeeId)
        {
            try
            {
                var tickets = await _ticketRepository
                    .GetByCreatedEmployeeIdAsync(employeeId);
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("assigned/{employeeId}")]
        public async Task<ActionResult> GetByAssignedEmployee(string employeeId)
        {
            var tickets = await _ticketRepository
                .GetByAssignedEmployeeIdAsync(employeeId);
            return Ok(tickets);
        }

        [HttpGet("status/{statusId}")]
        public async Task<ActionResult> GetByStatus(string statusId)
        {
            try
            {
                var tickets = await _ticketRepository
                    .GetByStatusIdAsync(statusId);
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("type/{typeId}")]
        public async Task<ActionResult> GetByTicketType(string typeId)
        {
            try
            {
                var tickets = await _ticketRepository
                    .GetByTicketTypeIdAsync(typeId);
                return Ok(tickets);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                await _ticketRepository.CreateTicketAsync(ticket);
                return CreatedAtAction(
                    nameof(GetTicket),
                    new { ticketId = ticket.TicketId },
                    ticket);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{ticketId}")]
        public async Task<ActionResult> UpdateTicket(string ticketId, Ticket ticket)
        {
            try
            {
                await _ticketRepository.UpdateTicketAsync(ticketId, ticket);
                return Ok("Ticket updated successfully");
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{ticketId}")]
        public async Task<ActionResult> DeleteTicket(string ticketId)
        {
            try
            {
                await _ticketRepository.DeleteTicketAsync(ticketId);
                return Ok("Ticket deleted successfully");
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
