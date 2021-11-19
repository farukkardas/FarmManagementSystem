using Business.Abstract;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLoginDto userLoginDto)
        {
            
            var userToLogin = _authService.Login(userLoginDto);
            
            
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            
            
            var result = _authService.CreateAccessToken(userToLogin.Data);
            
            if (result.Success)
            {
                return Ok(result);
            }

            
            return BadRequest(result.Message);
        }
        
        
        [HttpPost("register")]
        public ActionResult Register(UserRegisterDto userRegisterDto)
        {
            var userExists = _authService.UserExists(userRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authService.Register(userRegisterDto, userRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
    }
}