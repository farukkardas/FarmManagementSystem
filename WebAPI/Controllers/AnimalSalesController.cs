using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Hayvan satışlarının kayıtları için oluşturulan service.")]
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalSalesController : Controller
    {
        private readonly IAnimalSalesService _animalSalesService;

        public AnimalSalesController(IAnimalSalesService animalSalesService)
        {
            _animalSalesService = animalSalesService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _animalSalesService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(AnimalSales animalSales, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = await _animalSalesService.Add(animalSales, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(AnimalSales animalSales, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = await _animalSalesService.Update(animalSales, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(AnimalSales animalSales, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = await _animalSalesService.Delete(animalSales, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }


            return BadRequest(result);
        }

        [HttpGet("getuseranimalsales")]
        public async Task<IActionResult> GetUserAnimalSales([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _animalSalesService.GetUserAnimalSales(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}