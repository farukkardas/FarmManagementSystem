using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result =await _customerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customerService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("getcustomersummary")]
        public async Task<IActionResult> GetCustomerSummary()
        {
            var result = await _customerService.GetCustomerSummary();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Customer customer,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _customerService.Add(customer,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(Customer customer,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _customerService.Delete(customer,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("update")]
        public async Task<IActionResult> Update(Customer customer,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result = await _customerService.Update(customer,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getusercustomers")]
        public async Task<IActionResult> GetUserCustomer([FromHeader]int id, [FromHeader]string securityKey)
        {
            var result = await _customerService.GetUserCustomers(id, securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}