using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Exceptions;

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
               
                var newUser = _userService.AddNewUser(user);
        
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

            if (userIdClaim == null)
            {
                throw new UnauthorizedException("Usuario no autenticado.");
            }

            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }

            _userService.UpdateUser(userId, password);

            return Ok(new { message = "Password updated successfully." });
        }

        [HttpDelete]
        [Authorize] 
        public IActionResult DeleteUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedException("User ID claim not found.");
            }
            if (!int.TryParse(userIdClaim.Value, out int userId))
            {
                throw new BadRequestException("Invalid user ID format.");
            }
            _userService.DeleteUser(userId);
            return Ok(new { message = "User deleted successfully." });
        }
    }
}
