using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MilkSalesController : ControllerBase
    {
        private IMilkSalesService _milkSalesService;

        public MilkSalesController(IMilkSalesService milkSalesService)
        {
            _milkSalesService = milkSalesService;
        }
        
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _milkSalesService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getmilksales")]
        public IActionResult GetAllMilkSales()
        {
            var result = _milkSalesService.GetMilkSales();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

       

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _milkSalesService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(MilkSales milkSales,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _milkSalesService.Add(milkSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(MilkSales milkSales,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _milkSalesService.Delete(milkSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public IActionResult Update(MilkSales milkSales,[FromHeader]int id,[FromHeader] string securityKey)
        {
            var result = _milkSalesService.Update(milkSales,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getusermilksales")]
        public IActionResult GetUserMilkSales([FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = _milkSalesService.GetUserMilkSales(id,securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}