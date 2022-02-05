using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FertilizerController : Controller
    {
        private readonly IFertilizerService _fertilizerService;

        public FertilizerController(IFertilizerService fertilizerService)
        {
            _fertilizerService = fertilizerService;
        }
        
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _fertilizerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result =  await _fertilizerService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Fertilizer fertilizer,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _fertilizerService.Add(fertilizer,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Fertilizer fertilizer,[FromHeader]int id, [FromHeader]string securityKey)
        {
            var result = await _fertilizerService.Delete(fertilizer,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(Fertilizer fertilizer,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result =  await _fertilizerService.Update(fertilizer,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpGet("getuserfertilizers")]
        public async Task<IActionResult> GetUserFertilizers([FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _fertilizerService.GetUserFertilizers(id,securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}