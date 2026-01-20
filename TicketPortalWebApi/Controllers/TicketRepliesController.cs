using EFTicketPortalLibrary.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EFTicketPortalLibrary.Models;
using EFTicketPortalLibrary.Repos;

namespace TicketPortalWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketRepliesController : ControllerBase
    {
      ITicketRepliesRepository ticketrepliesRepo;

      public TicketRepliesController(ITicketRepliesRepository ticketRepliesRepository)
        {
            ticketrepliesRepo = ticketRepliesRepository;
        }

        [HttpGet]
        
            public async Task<ActionResult> GetAll()
        {
            List<TicketReplies> ticketreply = await ticketrepliesRepo.GetAllTicketRepliesAsync();
            return Ok(ticketreply); 
        }
        
    }
}
