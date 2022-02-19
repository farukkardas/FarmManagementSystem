using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   [Route("api/[controller]")]
  [ApiController]
    public class MilkSalesController : Controller
    {
        private IMilkSalesService _milkSalesService;

        public MilkSalesController(IMilkSalesService milkSalesService)
        {
            _milkSalesService = milkSalesService;
        }
        
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _milkSalesService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getmilksales")]
        public async Task<IActionResult> GetAllMilkSales()
        {
            var result = await _milkSalesService.GetMilkSales();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

       

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _milkSalesService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(MilkSales milkSales,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _milkSalesService.Add(milkSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(MilkSales milkSales,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = await _milkSalesService.Delete(milkSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(MilkSales milkSales,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result =  await _milkSalesService.Update(milkSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getusermilksales")]
        public async Task<IActionResult> GetUserMilkSales([FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _milkSalesService.GetUserMilkSales(id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}