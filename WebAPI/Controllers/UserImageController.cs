using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Kullanıcı resim ekleme servisi.")]
    [Route("api/[controller]")]
   [ApiController]
    public class UserImageController : Controller
    {
        readonly IUserImageService _userImageService;

        public UserImageController(IUserImageService userImageService)
        {
            _userImageService = userImageService;
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromForm]ImageUpload file,[FromForm]UserImage userImage,[FromHeader]int id,[FromHeader]string securityKey)
        {
            
            var result =await _userImageService.Add(file.File,userImage,id,securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }
            
            return BadRequest(result);
        }
        
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int imageId,[FromHeader]int id,[FromHeader]string securityKey)
        { 

            var userImage = await _userImageService.Get(id);

            var result = await _userImageService.Delete(userImage.Data,id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromForm]ImageUpload file,[FromForm] UserImage userImage,[FromHeader]int id ,[FromHeader]string securityKey)
        {
           
            var result = await _userImageService.Update(file.File, userImage,id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById([FromForm(Name = ("Id"))] int id)
        {
            var result = await _userImageService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        
        [HttpGet("getimagesbyuserid")]
        public async Task<IActionResult> GetImagesByUserId([FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _userImageService.GetImagesByUserId(id,securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}