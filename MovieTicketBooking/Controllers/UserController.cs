using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieTicketBooking.Business.Repository;
using MovieTicketBooking.Business.Service;
using MovieTicketBooking.Data.Models;
using System.Security.Claims;

namespace MovieTicketBooking.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;

        
        public UserController(IUserRepository repository, IUserService service)
        {
            _repository = repository;
            _service = service;
        }

       
        [HttpPost]
        [Route("Create")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data invallid");
            }

            PrepareResponse response = await _service.CreateUser(model);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

       
        [HttpPost]
        [Route("Create/Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAdmin(UserDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Data invallid");
            }

            PrepareResponse response = await _service.CreateUser(model, true);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

       
        [Route("update/password")]
        [HttpPatch]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword userPassword)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("No user found");
            }

            var currentUser = await _repository.GetUserByUsername(userPassword.Username);

            var response = await _repository.ForgotPassword(userPassword, currentUser);

            if (response.status)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
}
