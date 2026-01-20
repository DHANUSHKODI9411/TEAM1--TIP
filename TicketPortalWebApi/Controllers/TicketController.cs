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
        ITicketRepository ticketRepo;
        public TicketController(ITicketRepository ticketRepository)
        {
            ticketRepo = ticketRepository;
        }
        [HttpGet]
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<Ticket> tickets = await ticketRepo.GetAllTicketsAsync();
            return Ok(tickets.ToList());
        }
        [HttpGet("{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string ticketId)
        {
            try
            {
                Ticket ticket = await ticketRepo.GetTicketAsync(ticketId);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("bycreatedemployee/{createdEmployeeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByCreatedEmployee(string createdEmployeeId)
        {
            try
            {
                IEnumerable<Ticket> tickets = await ticketRepo.GetByCreatedEmployeeIdAsync(createdEmployeeId);
                return Ok(tickets.ToList());
            }
            catch(TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("byassignedemployee/{assignedEmployeeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByAssignedEmployee(string assignedEmployeeId)
        {
            try
            {
                IEnumerable<Ticket> tickets = await ticketRepo.GetByAssignedEmployeeIdAsync(assignedEmployeeId);
                return Ok(tickets.ToList());
            }
            catch(TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet("bystatus/{statusId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetByStatus(string statusId)
        {
            try
            {
                IEnumerable<Ticket> tickets = await ticketRepo.GetByStatusIdAsync(statusId);
                return Ok(tickets.ToList());
            }
            catch(TicketException ex)
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
                IEnumerable<Ticket> tickets = await ticketRepo.GetByTicketTypeIdAsync(ticketTypeId);
                return Ok(tickets.ToList());
            }
            catch(TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(Ticket ticket)
        {
            try
            {
                await ticketRepo.CreateTicketAsync(ticket);
                return Created($"api/ticket/{ticket.TicketId}", ticket);
            }
            catch (TicketException ex)
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
                await ticketRepo.UpdateTicketAsync(ticketId, ticket);
                return Ok(ticket);
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 502)
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
                await ticketRepo.DeleteTicketAsync(ticketId);
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

 