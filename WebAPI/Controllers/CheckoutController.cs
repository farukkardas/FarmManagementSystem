using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Ödeme işlemleri demo servisi.")]
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
        public async Task<IActionResult> CheckoutProducts([FromHeader] int id, [FromHeader] string securityKey,
            CreditCart creditCart)
        {
            var result = await _checkOutService.CheckoutProducts(id, securityKey, creditCart);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}