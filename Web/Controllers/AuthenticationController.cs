using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration config, ICustomAuthenticationService autenticacionService)
        {
            _config = config; 
            _customAuthenticationService = autenticacionService;
        }

        [HttpPost("authenticate")] 
        public ActionResult<string> Autenticar(UserLoginRequest loginRequest) 
        {
            string token = _customAuthenticationService.Autenticar(loginRequest); 

            return Ok(new TokenResponse { Token = token });
        }

    }
}
