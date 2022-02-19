using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Business.Abstract;
using Entities.Concrete;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Stoktaki buzağı kayıtları. (BULL)")]
    [Route("api/[controller]")]
    [ApiController]
    public class CalvesController : Controller
    {
        readonly ICalfService _calvesService;

        public CalvesController(ICalfService calvesService)
        {
            _calvesService = calvesService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _calvesService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _calvesService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getusercalves")]
        public async Task<IActionResult> GetUserCalves([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _calvesService.GetUserCalves(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Calves calves, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _calvesService.Add(calves, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Calves calves, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _calvesService.Delete(calves, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Calves calves, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _calvesService.Update(calves, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}