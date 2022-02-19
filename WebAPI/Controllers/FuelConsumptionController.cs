using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
     [ApiController]
    public class FuelConsumptionController : Controller
    {
        private readonly IFuelConsumptionService _fuelConsumptionService;

        public FuelConsumptionController(IFuelConsumptionService fuelConsumptionService)
        {
            _fuelConsumptionService = fuelConsumptionService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _fuelConsumptionService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _fuelConsumptionService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(FuelConsumption fuelConsumption,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _fuelConsumptionService.Add(fuelConsumption,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(FuelConsumption fuelConsumption,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _fuelConsumptionService.Delete(fuelConsumption,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(FuelConsumption fuelConsumption,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result =await _fuelConsumptionService.Update(fuelConsumption,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getuserfuelconsumption")]
        public async Task<IActionResult> GetUserFuelConsumption([FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _fuelConsumptionService.GetUserFuelConsumptions(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}