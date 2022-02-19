using System.Threading.Tasks;
using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Entities.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Stoktaki hayvan kayıtları. (Cow)")]
    [Route("api/[controller]")]
    [ApiController]
    public class CowController : Controller
    {
        private readonly ICowService _cowService;

        public CowController(ICowService cowService)
        {
            _cowService = cowService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cowService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cowService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Cow cow, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _cowService.Add(cow, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Cow cow, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _cowService.Delete(cow, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(Cow cow, [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _cowService.Update(cow, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getusercows")]
        public async Task<IActionResult> GetUserCows([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _cowService.GetUserCows(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}