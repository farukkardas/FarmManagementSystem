using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [SwaggerTag("Sepet işlemleri servisi.")]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("getbasketproducts")]
        public async Task<IActionResult> GetBasketProducts([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _basketService.GetBasketProducts(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addtobasket")]
        public async Task<IActionResult> AddToBasket(ProductInBasket productInBasket, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = await _basketService.AddToBasket(productInBasket, id, securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("deletetobasket")]
        public async Task<IActionResult> DeleteToBasket(ProductInBasket productInBasket, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = await _basketService.DeleteFromBasket(productInBasket, id, securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}