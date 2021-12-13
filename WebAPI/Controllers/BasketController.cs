using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : Controller
    {
        private IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("getbasketproducts")]
        public IActionResult GetBasketProducts([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = _basketService.GetBasketProducts(id, securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("addtobasket")]
        public IActionResult AddToBasket(ProductInBasket productInBasket, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = _basketService.AddToBasket(productInBasket, id, securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpPost("deletetobasket")]
        public IActionResult DeleteToBasket(ProductInBasket productInBasket, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = _basketService.DeleteFromBasket(productInBasket, id, securityKey);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}