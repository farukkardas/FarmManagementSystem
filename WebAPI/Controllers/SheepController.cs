using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SheepController : ControllerBase
    {
        private readonly ISheepService _sheepService;

        public SheepController(ISheepService sheepService)
        {
            _sheepService = sheepService;
        }


        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _sheepService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _sheepService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Sheep sheep,int id, string securityKey)
        {
            var result = _sheepService.Add(sheep,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Sheep sheep,int id, string securityKey)
        {
            var result = _sheepService.Delete(sheep,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Sheep sheep,int id, string securityKey)
        {
            var result = _sheepService.Update(sheep,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getusersheeps")]
        public IActionResult GetUserSheeps(int id, string securityKey)
        {
            var result = _sheepService.GetUserSheeps(id, securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}