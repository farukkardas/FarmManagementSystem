using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelConsumptionController : ControllerBase
    {
        private readonly IFuelConsumptionService _fuelConsumptionService;

        public FuelConsumptionController(IFuelConsumptionService fuelConsumptionService)
        {
            _fuelConsumptionService = fuelConsumptionService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _fuelConsumptionService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _fuelConsumptionService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(FuelConsumption fuelConsumption,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _fuelConsumptionService.Add(fuelConsumption,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(FuelConsumption fuelConsumption,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _fuelConsumptionService.Delete(fuelConsumption,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(FuelConsumption fuelConsumption,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _fuelConsumptionService.Update(fuelConsumption,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getuserfuelconsumption")]
        public IActionResult GetUserFuelConsumption([FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _fuelConsumptionService.GetUserFuelConsumptions(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}