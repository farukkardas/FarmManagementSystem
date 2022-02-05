using System.Threading.Tasks;
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
        public async Task<IActionResult> GetUserOrders([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result =  await _orderService.GetUserOrders(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("GetCustomerOrders")]
        public async Task<IActionResult> GetCustomerOrders([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _orderService.GetCustomerOrders(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _orderService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("approveorder")]
        public async Task<IActionResult> ApproveOrder([FromHeader] int id, [FromHeader] string securityKey, [FromForm] int order)
        {
            var result = await _orderService.ApproveOrder(id, securityKey, order);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPut("cancelorder")]
        public async Task<IActionResult> CancelOrder([FromHeader] int id, [FromHeader] string securityKey, [FromForm] int orderId)
        {
            var result =  await _orderService.CancelOrder(orderId, id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut("addcargono")]
        public async Task<IActionResult> AddCargoNo([FromHeader] int id, [FromHeader] string securityKey, [FromForm] int order, [FromForm]int deliveryNo)
        {
            var result = await _orderService.AddCargoNumber(id, securityKey, order,deliveryNo);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}