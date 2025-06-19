using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Api.Core.Entities;
using StockManagement.Api.Core.Interfaces;
using StockManagement.Api.Dtos;

namespace StockManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {

        public IConfiguration _configuration;

        private readonly IConfiguration _config;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public TokenController(IConfiguration config, IAuthService tokenService, IUserRepository userRepository)
        {
            _configuration = config;


            _authService = tokenService;
            _userRepository = userRepository;
            _config = config;
        }



        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest userModel)
        {
            if (string.IsNullOrEmpty(userModel.Email) || string.IsNullOrEmpty(userModel.Password))
            {
                return BadRequest("Email y password Raqueridos");
            }
            IActionResult response = Unauthorized();
            User user = new User { Email = userModel.Email, Password = userModel.Password };
            var validUser = _userRepository.GetUser(user);

            var fechaActual = DateTime.UtcNow;
            var validez = TimeSpan.FromHours(5);
            if (validUser != null)
            {
                var generatedToken = _authService.GenerateToken(fechaActual, validUser, validez);
                if (generatedToken != null)
                {
                    return Ok(new { Token = generatedToken });
                    // return Ok(generatedToken);

                }
                //else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
