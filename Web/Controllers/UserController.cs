﻿using Application.Interfaces;
using Application.Models.Requests;
using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Domain.Exceptions.NotAllowedExceptions;

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
        public IActionResult AddUser([FromBody] UserCreateRequest user)
        {
            try
            {
                var newUser = _userService.AddNewUser(user);
                return Ok(newUser);
            }
            catch (NotAllowedException ex)
            {
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
                return StatusCode(500, new { message = "Error interno del servidor" });
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
