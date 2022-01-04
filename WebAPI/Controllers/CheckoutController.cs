using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : Controller
    {
        private readonly ICheckOutService _checkOutService;

        public CheckoutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }

        [HttpPost("checkoutproducts")]
        public IActionResult CheckoutProducts([FromHeader]int id,[FromHeader] string securityKey, CreditCart creditCart)
        {
            var result = _checkOutService.CheckoutProducts(id, securityKey, creditCart);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}