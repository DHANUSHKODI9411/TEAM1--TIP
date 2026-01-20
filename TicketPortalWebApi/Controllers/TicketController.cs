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

        // GET: api/ticket
        [HttpGet]
        public async Task<ActionResult> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            return Ok(tickets);
        }

        // GET: api/ticket/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTicketById(string id)
        {
            try
            {
                var ticket = await _ticketRepository.GetTicketByIdAsync(id);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // GET: api/ticket/created/{empId}
        [HttpGet("created/{empId}")]
        public async Task<ActionResult> GetByCreatedEmployee(string empId)
        {
            var tickets = await _ticketRepository.GetByCreatedEmployeeIdAsync(empId);
            return Ok(tickets);
        }

        // GET: api/ticket/assigned/{empId}
        [HttpGet("assigned/{empId}")]
        public async Task<ActionResult> GetByAssignedEmployee(string empId)
        {
            var tickets = await _ticketRepository.GetByAssignedEmployeeIdAsync(empId);
            return Ok(tickets);
        }

        // GET: api/ticket/status/{statusId}
        [HttpGet("status/{statusId}")]
        public async Task<ActionResult> GetByStatus(string statusId)
        {
            var tickets = await _ticketRepository.GetByStatusIdAsync(statusId);
            return Ok(tickets);
        }

        // GET: api/ticket/type/{typeId}
        [HttpGet("type/{typeId}")]
        public async Task<ActionResult> GetByTicketType(string typeId)
        {
            var tickets = await _ticketRepository.GetByTicketTypeIdAsync(typeId);
            return Ok(tickets);
        }

        // POST: api/ticket
        [HttpPost]
        public async Task<ActionResult> CreateTicket(Ticket ticket)
        {
            try
            {
                await _ticketRepository.CreateTicketAsync(ticket);
                return CreatedAtAction(nameof(GetTicketById),
                    new { id = ticket.TicketId }, ticket);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/ticket
        [HttpPut]
        public async Task<ActionResult> UpdateTicket(Ticket ticket)
        {
            try
            {
                await _ticketRepository.UpdateTicketAsync(ticket);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 404)
                    return NotFound(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ticket/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTicket(string id)
        {
            try
            {
                await _ticketRepository.DeleteTicketAsync(id);
                return Ok("Ticket deleted successfully");
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 404)
                    return NotFound(ex.Message);

                return Conflict(ex.Message);
            }
        }
    }
}
