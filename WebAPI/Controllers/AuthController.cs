﻿using Business.Abstract;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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
               
               Log.Error($"{userLoginDto.Email} sucessfully logged!");  
                return Ok(result);
                
            }

            
            return BadRequest(result.Message);
        }
        
        
        [HttpPost("register")]
        public ActionResult Register(UserRegisterDto userRegisterDto)
        {
       
            var registerResult = _authService.Register(userRegisterDto, userRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult.Data);
            
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("checkskoutdated")]
        public ActionResult CheckSecurityKeyOutdated(int id)
        {
           var result = _authService.CheckSecurityKeyOutdated(id);

           if (result.Success)
           {
               return Ok(result);
           }

           return BadRequest(result.Message);
        }
    }
}