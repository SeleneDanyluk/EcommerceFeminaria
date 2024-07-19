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
                try
                {
                    // Log the incoming request
                    Console.WriteLine($"Attempting to add user: {user.Email}");

                    var newUser = _userService.AddNewUser(user);

                    // Log successful creation
                    Console.WriteLine($"User added successfully");

                    return Ok(newUser);
                }
                catch (NotAllowedException ex)
                {
                    Console.WriteLine($"NotAllowedException: {ex.Message}");
                    if (ex.Message.Contains("Email existente"))
                    {
                        return BadRequest(new { message = "El email ya está registrado. Por favor, intente con otro." });
                    }
                    else
                    {
                        return BadRequest(new { message = "Error interno del servidor. Intente nuevamente" });
                    }
                }
                catch (Exception ex)
                {
                    // Log the full exception details
                    Console.WriteLine($"Unexpected error in AddUser: {ex}");
                    return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
                }
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

            try
            {
                // Log the incoming request
                Console.WriteLine($"Attempting to update password for user ID: {userId}");

                _userService.UpdateUser(userId, password);

                // Log successful update
                Console.WriteLine($"Password updated successfully for user ID: {userId}");

                return Ok(new { message = "Password updated successfully." });
            }
            catch (NotAllowedException ex)
            {
                Console.WriteLine($"NotAllowedException: {ex.Message}");
                return BadRequest(new { message = "No se permite actualizar la contraseña. " + ex.Message });
            }
            catch (Exception ex)
            {
                // Log the full exception details
                Console.WriteLine($"Unexpected error in UpdateUser: {ex}");
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message });
            }
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

            try
            {
                _userService.DeleteUser(userId);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { message = "An error occurred while deleting the user." });
            }
        }
    }
}
