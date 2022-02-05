using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsOnSaleController : Controller
    {
        private readonly IProductsOnSaleService _productsOnSaleService;

        public ProductsOnSaleController(IProductsOnSaleService productsOnSaleService)
        {
            _productsOnSaleService = productsOnSaleService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productsOnSaleService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productsOnSaleService.GetById(id);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("addproduct")]
        public async Task<IActionResult> Add([FromForm]ProductsOnSale productsOnSale,[FromForm]IFormFile file,[FromHeader]int id ,[FromHeader]string securityKey)
        {
            var result = await _productsOnSaleService.Add(productsOnSale,file,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getuserproducts")]
        public async Task<IActionResult> GetUserProducts([FromHeader]int id ,[FromHeader]string securityKey)
        {
            var result = await _productsOnSaleService.GetUserProducts(id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        
        [HttpPost("deleteproduct")]
        public async Task<IActionResult> Delete([FromForm]int productId,[FromHeader]int id,[FromHeader]string securityKey)
        {
            var result =  await _productsOnSaleService.Delete(productId,id,securityKey);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}