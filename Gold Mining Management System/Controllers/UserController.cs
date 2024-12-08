using Gold_Mining_Management_System.Data;
using Gold_Mining_Management_System.DTO;
using Gold_Mining_Management_System.Models;
using Gold_Mining_Management_System.Repositories;
using Gold_Mining_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gold_Mining_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext context;

        public UserController(IUserService userService, IUserRepository userRepository, AppDbContext context)
        {
            _userService = userService;
            _userRepository = userRepository;
            this.context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users user)
        {
            if (!await _userService.RegisterUser(user))
                return BadRequest("User already exists.");
            try
            {
                var response = new
                {
                    status = "success",
                    message = "User registered successfully",
                    data = user // Returning the user data or userId
                };

                return CreatedAtAction(nameof(Register), new { id = user.Username }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { status = "error", message = "Registration failed", error = ex.Message });
            }  
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Users u)
        {
            var token = await _userService.AuthenticateUser(u);
            if (token == null)
                return Unauthorized("Invalid credentials.");
            return Ok(new { Token = token });
        }

        [HttpGet("Counts")]
        [AllowAnonymous]
        public async Task<ActionResult<UserCounts>> GetUserCounts()
        {
            var userCounts = await _userService.GetUserCountsAsync();
            return Ok(userCounts);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public IActionResult GetUsers()
        {
            var res = context.Users.ToList();
            return Ok(res);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Mine Manager, Geologist, Engineer, Safety Officer, Field Worker")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound(); 
            }

            return Ok(user);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Users updatedUser)
        {
            if (updatedUser == null || updatedUser.UserId != id)
            {
                return BadRequest("User data is invalid.");
            }

            var result = await _userRepository.UpdateUser(id, updatedUser);

            if (!result)
            {
                return NotFound(); 
            }

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userDeleted = await _userRepository.DeleteUser(id);
            if (!userDeleted)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(new { message = $"User with ID {id} has been deleted." });
        }
    }
}
