using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalSalesController : Controller
    {
        private readonly IAnimalSalesService _animalSalesService;

        public AnimalSalesController(IAnimalSalesService animalSalesService)
        {
            _animalSalesService = animalSalesService;
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _animalSalesService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("add")]
        public IActionResult Add(AnimalSales animalSales,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _animalSalesService.Add(animalSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(AnimalSales animalSales,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _animalSalesService.Update(animalSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("delete")]
        public IActionResult Delete(AnimalSales animalSales,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _animalSalesService.Delete(animalSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getuseranimalsales")]
        public IActionResult GetUserAnimalSales([FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _animalSalesService.GetUserAnimalSales(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}