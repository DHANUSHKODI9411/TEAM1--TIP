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
    public class StatusController : ControllerBase
    {
        IStatusRepository _statusRepository;
        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var statuses = await _statusRepository.GetAllStatusesAsync();
            return Ok(statuses);
        }
        [HttpGet("{statusId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string statusId)
        {
            try
            {
                var status = await _statusRepository.GetStatusAsync(statusId);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Add(Status status)
        {
            try
            {
                await _statusRepository.AddStatusAsync(status);
                return Created($"api/status/{status.StatusId}", status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{statusId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Update(string statusId, Status status)
        {
            try
            {
                await _statusRepository.UpdateStatusAsync(statusId, status);
                return Ok(status);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("not found"))
                    return NotFound(ex.Message);
                else
                    return BadRequest(ex.Message);
            }
        }
 
        [HttpDelete("{statusId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(string statusId)
        {
            try
            {
                await _statusRepository.DeleteStatusAsync(statusId);
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

 