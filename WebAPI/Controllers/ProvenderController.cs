using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvenderController : ControllerBase
    {
        private readonly IProvenderService _provenderService;

        public ProvenderController(IProvenderService provenderService)
        {
            _provenderService = provenderService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _provenderService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _provenderService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Provender provender,int id, string securityKey)
        {
            var result = _provenderService.Add(provender,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Provender provender,int id, string securityKey)
        {
            var result = _provenderService.Delete(provender,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(Provender provender,int id, string securityKey)
        {
            var result = _provenderService.Update(provender,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getuserprovenders")]
        public IActionResult GetUserProvenders(int id, string securityKey)
        {
            var result = _provenderService.GetUserProvenders(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}