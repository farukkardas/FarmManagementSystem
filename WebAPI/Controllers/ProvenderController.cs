using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvenderController : Controller
    {
        private readonly IProvenderService _provenderService;

        public ProvenderController(IProvenderService provenderService)
        {
            _provenderService = provenderService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _provenderService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _provenderService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Provender provender,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _provenderService.Add(provender,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Provender provender,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _provenderService.Delete(provender,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(Provender provender,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _provenderService.Update(provender,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getuserprovenders")]
        public async Task<IActionResult> GetUserProvenders([FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _provenderService.GetUserProvenders(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}