using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        readonly IUserImageService _userImageService;

        public UserImageController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }


        [HttpPost("add")]
        public IActionResult Add([FromForm]IFormFile file,[FromForm]UserImage userImage,[FromHeader]int id,[FromHeader]string securityKey)
        {
            
            var result = _userImageService.Add(file,userImage,id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        
        [HttpDelete("delete")]
        public IActionResult Delete(int imageId,[FromHeader]int id,[FromHeader]string securityKey)
        {

            var userImage = _userImageService.Get(id).Data;

            var result = _userImageService.Delete(userImage,id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm]IFormFile file,[FromForm] UserImage userImage,[FromHeader]int id ,[FromHeader]string securityKey)
        {
           
            var result = _userImageService.Update(file, userImage,id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById([FromForm(Name = ("Id"))] int id)
        {
            var result = _userImageService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        
        [HttpGet("getimagesbyuserid")]
        public IActionResult GetImagesByUserId([FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _userImageService.GetImagesByUserId(id,securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}