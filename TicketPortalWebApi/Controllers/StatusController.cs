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
        IStatusRepository statusRepo;
        public StatusController(IStatusRepository statusRepository)
        {
            statusRepo = statusRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Status> statuses = await statusRepo.GetAllStatusesAsync();
            return Ok(statuses);
        }
        [HttpGet("{statusId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> GetOne(string statusId)
        {
            try
            {
                Status status = await statusRepo.GetStatusAsync(statusId);
                return Ok(status);
            }
            catch (TicketException ex)
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
                await statusRepo.AddStatusAsync(status);
                return Created($"api/status/{status.StatusId}", status);
            }
            catch (TicketException ex)
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
                await statusRepo.UpdateStatusAsync(statusId, status);
                return Ok(status);
            }
            catch (TicketException ex)
            {
                if (ex.ErrorNumber == 502)
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
                await statusRepo.DeleteStatusAsync(statusId);
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

 