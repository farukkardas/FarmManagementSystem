using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        public UsersController(IUserService userService, IAuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(User user)
        {
            var result = _userService.Add(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(User user,int id,string securityKey)
        {
            var result = _userService.Delete(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPut("update")]
        public IActionResult Update(User user,[FromHeader]int id ,[FromHeader] string securityKey)
        {
            IResult conditionResult = _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }
            
            var result = _userService.Update(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("updateuser")]
        public IActionResult UpdateUser(UserForEdit userForEdit,[FromHeader]int id , [FromHeader]string securityKey)
        {
            IResult conditionResult = _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }
            
            var result = _userService.UpdateUser(userForEdit);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getuserdetails")]
        public IActionResult GetUserDetails([FromHeader]int id,[FromHeader]string securityKey)
        {
            
            IResult conditionResult = _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }
            
            var result = _userService.GetUserDetails(id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        
    }
}