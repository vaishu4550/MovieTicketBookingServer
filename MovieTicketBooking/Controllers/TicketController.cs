using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Business.Service;
using MovieTicketBooking.Data.Models.Dto;
using System.Security.Claims;

namespace MovieTicketBooking.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _service;

        
        public TicketController(ITicketService service)
        {
            _service = service;
        }

       
        [HttpPost]
        [Route("Book")]
        public async Task<IActionResult> TicketBooking([FromBody] TicketDto model)
        {
            var userId = User.FindFirstValue("Id");
            var response = await _service.TicketBook(model, userId, model.TheatreId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
