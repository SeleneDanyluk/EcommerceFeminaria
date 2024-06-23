using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAllUsers());
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserCreatedRequest user)
        {
            try
            {
                return Ok(_userService.AddNewUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "El email ya está registrado", error = ex.Message });
            }
        }

        [HttpGet("/email")]
        public IActionResult GetByEmail([FromQuery] string email)
        {
            return Ok(_userService.GetUserByEmail(email));
        }

        [HttpPut("/password")]
        public IActionResult UpdateUser([FromQuery] int id, string password)
        {
            try
            {
                _userService.UpdateUser(id, password);
                return Ok(new { message = "Password updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while updating the password.", error = ex.Message });
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok(new { message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred while deleting the user.", error = ex.Message });
            }

        }
    }
}
