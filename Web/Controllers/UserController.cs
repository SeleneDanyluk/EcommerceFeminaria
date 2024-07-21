using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Exceptions.NotAllowedExceptions;
using System.Security.Claims;
using System.Linq;
using Domain.Enum;
using Domain.Entities;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

            if (userTypeString != "superAdmin")
            {
                return Forbid();
            }

            return Ok(_userService.GetAllUsers());
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddUser([FromBody] UserCreateRequest user)

        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (user.UserType == 0 || (userTypeString == "superAdmin"))
            {
                Console.WriteLine($"Attempting to add user: {user.Email}");
                var newUser = _userService.AddNewUser(user);
                Console.WriteLine($"User added successfully");
                return Ok(newUser);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpGet("/email")]
        [Authorize]
        public IActionResult GetByEmail([FromQuery] string email)
        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
            if (userTypeString == "superAdmin")
            {
                return Ok(_userService.GetUserByEmail(email));
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("/password")]
        [Authorize]
        public IActionResult UpdateUser([FromBody] string password)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return Unauthorized(new { message = "Invalid user identification." });
            }

            if (string.IsNullOrEmpty(userTypeString))
            {
                return Forbid();
            }

            Console.WriteLine($"Attempting to update password for user ID: {userId}");
            _userService.UpdateUser(userId, password);
            Console.WriteLine($"Password updated successfully for user ID: {userId}");

            return Ok(new { message = "Password updated successfully." });
        }

        [HttpDelete]
        [Authorize] 
        public IActionResult DeleteUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "User ID claim not found." });
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                return BadRequest(new { message = "Invalid user ID format." });
            }

            _userService.DeleteUser(userId);
            return Ok(new { message = "User deleted successfully." });
        }
    }
}
