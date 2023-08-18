using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Business.Service;
using MovieTicketBooking.Data.Models.Dto;

namespace MovieTicketBooking.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service)
        {
            _service = service;
        }

       
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> CreateMovie(MovieDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }

            var response = await _service.Create(model);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

       
        [HttpGet]
        [Route("Reterive/All")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllMovie()
        {
            var movies = await _service.GetMovie();
            return movies.Count > 0 ? Ok(movies) : BadRequest();
        }

        
        [HttpGet]
        [Route("Reterive/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMovieById([FromRoute] string id)
        {
            var movie = await _service.GetMovie(id);
            return movie != null ? Ok(movie) : BadRequest();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteMovie([FromRoute] string id)
        {
            var movie = await _service.GetMovie(id);
            return movie != null ? Ok(movie) : BadRequest();
        }
    }
}
