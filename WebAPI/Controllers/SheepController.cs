using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Stoktaki koyun kayıtları.")]
    [Route("api/[controller]")]
    [ApiController]
    public class SheepController : Controller
    {
        private readonly ISheepService _sheepService;

        public SheepController(ISheepService sheepService)
        {
            _sheepService = sheepService;
        }


        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _sheepService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sheepService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Sheep sheep, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _sheepService.Add(sheep, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Sheep sheep, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _sheepService.Delete(sheep, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Sheep sheep, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _sheepService.Update(sheep, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getusersheeps")]
        public async Task<IActionResult> GetUserSheeps([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _sheepService.GetUserSheeps(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}