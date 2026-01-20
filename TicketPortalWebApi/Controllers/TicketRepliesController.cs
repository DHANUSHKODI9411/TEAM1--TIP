using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;
using Microsoft.AspNetCore.Mvc;
 
namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketRepliesController : ControllerBase
    {
        private readonly ITicketRepliesRepository ticketrepliesRepo;
 
        public TicketRepliesController(ITicketRepliesRepository ticketRepliesRepository)
        {
            ticketrepliesRepo = ticketRepliesRepository;
        }
 
        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult> GetAll()
        {
            IEnumerable<TicketReplies> replies = await ticketrepliesRepo.GetAllTicketRepliesAsync();
            return Ok(replies.ToList());
        }
 
        [HttpGet("{replyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string replyId)
        {
            try
            {
                TicketReplies reply = await ticketrepliesRepo.GetTicketReplyAsync(replyId);
                return Ok(reply);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }
 
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(TicketReplies ticketReplies)
        {
            try
            {
                await ticketrepliesRepo.AddTicketReplyAsync(ticketReplies);
                return Created($"api/TicketReplies/{ticketReplies.ReplyId}", ticketReplies);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpDelete("{replyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string replyId)
        {
            try
            {
                await ticketrepliesRepo.DeleteTicketReplyAsync(replyId);
                return Ok();
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 502)
                    return NotFound(ex.Message);
 
                return BadRequest(ex.Message);
            }
        }
 
        [HttpPut("{replyId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string replyId, TicketReplies reply)
        {
            try
            {
                await ticketrepliesRepo.UpdateTicketReplyAsync(replyId, reply);
                return Ok();
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 502)
                    return NotFound(ex.Message);
 
                return BadRequest(ex.Message);
            }
        }
 
        [HttpGet("ticket/{ticketId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetByTicket(string ticketId)
        {
            try
            {
                IEnumerable<TicketReplies> replies = await ticketrepliesRepo.GetRepliesByTicketAsync(ticketId);
                return Ok(replies.ToList());
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 504)
                    return NotFound(ex.Message);
 
                return BadRequest(ex.Message);
            }
        }
 
        [HttpGet("created-employee/{employeeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetByCreatedEmployee(string employeeId)
        {
            try
            {
                IEnumerable<TicketReplies> replies = await ticketrepliesRepo.GetRepliesByCreatedEmployeeIdAsync(employeeId);
                return Ok(replies.ToList());
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
 
        [HttpGet("assigned-employee/{assignedEmployeeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> GetByAssignedEmployee(string assignedEmployeeId)
        {
            try
            {
                IEnumerable<TicketReplies> replies = await ticketrepliesRepo.GetRepliesByAssignedEmployeeIdAsync(assignedEmployeeId);
                return Ok(replies.ToList());
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}