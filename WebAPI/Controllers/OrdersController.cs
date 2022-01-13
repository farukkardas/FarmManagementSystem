using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        [HttpGet("GetUserOrders")]
        public IActionResult GetUserOrders([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = _orderService.GetUserOrders(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpGet("GetCustomerOrders")]
        public IActionResult GetCustomerOrders([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = _orderService.GetCustomerOrders(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpGet("GetAll")]
        public IActionResult GetAll([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = _orderService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("approveorder")]
        public IActionResult ApproveOrder([FromHeader] int id, [FromHeader] string securityKey,[FromForm]int order)
        {
            var result = _orderService.ApproveOrder(id, securityKey, order);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPut("addcargono")]
        public IActionResult AddCargoNo([FromHeader] int id, [FromHeader] string securityKey,int order,int deliveryNo)
        {
            var result = _orderService.AddCargoNumber(id, securityKey, order,deliveryNo);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}