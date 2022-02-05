using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BullController : Controller
    {
        private readonly IBullService _bullService;

        public BullController(IBullService bullService)
        {
            _bullService = bullService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _bullService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _bullService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getuserbulls")]
        public async Task<IActionResult> GetUserBulls([FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _bullService.GetUserBulls(id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Bull bull,[FromHeader]int id,[FromHeader]string securityKey)
        {
            
            var result = await _bullService.Add(bull,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Bull bull,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _bullService.Delete(bull,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(Bull bull,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _bullService.Update(bull,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
       
    }
}