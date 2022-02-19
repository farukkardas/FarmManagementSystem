using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Kullanıcı CRUD işlemleri.")]
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
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(User user)
        {
            var result = await _userService.Add(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(User user, int id, string securityKey)
        {
            IResult conditionResult = await _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }

            var result = await _userService.Delete(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(User user, [FromHeader] int id, [FromHeader] string securityKey)
        {
            IResult conditionResult = await _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }

            var result = await _userService.Update(user);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser(UserForEdit userForEdit, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            IResult conditionResult = await _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }

            var result = await _userService.UpdateUser(userForEdit);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getuserdetails")]
        public async Task<IActionResult> GetUserDetails([FromHeader] int id, [FromHeader] string securityKey)
        {
            IResult conditionResult = await _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }

            var result = await _userService.GetUserDetails(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("changeaddress")]
        public async Task<IActionResult> ChangeUserAddress([FromHeader] int id, [FromHeader] string securityKey,
            [FromForm] int cityId, [FromForm] string fullAddress)
        {
            IResult conditionResult = await _authService.UserOwnControl(id, securityKey);

            if (!conditionResult.Success)
            {
                return BadRequest(conditionResult);
            }

            var result = await _userService.ChangeUserAddress(id, securityKey, cityId, fullAddress);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}