using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Business.Service;
using MovieTicketBooking.Data.Models;

namespace MovieTicketBooking.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TheatreController : ControllerBase
    {
        private readonly ITheatreService service;

        
        public TheatreController(ITheatreService _service)
        {
            service = _service;
        }

        
        [HttpGet]
        [Route("Reterive/All")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTheatre()
        {
            return Ok(await service.GetTheatre());
        }

        
        [HttpGet]
        [Route("Reterive/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTheatreById([FromRoute] string id)
        {
            return Ok(await service.GetTheatre(id));
        }

        
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddTheatre(TheatreDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            var response = await service.AddTheatre(model);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
