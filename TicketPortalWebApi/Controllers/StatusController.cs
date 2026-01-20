using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStatuses()
        {
            var statuses = await _statusRepository.GetAllStatusesAsync();
            return Ok(statuses);
        }

        [HttpGet("{statusId}")]
        public async Task<ActionResult> GetStatus(string statusId)
        {
            try
            {
                var status = await _statusRepository.GetStatusAsync(statusId);
                return Ok(status);
            }
            catch (TicketException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddStatus(Status status)
        {
            try
            {
                await _statusRepository.AddStatusAsync(status);
                return CreatedAtAction(
                    nameof(GetStatus),
                    new { statusId = status.StatusId },
                    status);
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{statusId}")]
        public async Task<ActionResult> UpdateStatus(string statusId, Status status)
        {
            try
            {
                await _statusRepository.UpdateStatusAsync(statusId, status);
                return Ok("Status updated successfully");
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{statusId}")]
        public async Task<ActionResult> DeleteStatus(string statusId)
        {
            try
            {
                await _statusRepository.DeleteStatusAsync(statusId);
                return Ok("Status deleted successfully");
            }
            catch (TicketException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
