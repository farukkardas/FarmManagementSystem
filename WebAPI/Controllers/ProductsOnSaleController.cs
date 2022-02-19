using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsOnSaleController : Controller
    {
        private readonly IProductsOnSaleService _productsOnSaleService;

        public ProductsOnSaleController(IProductsOnSaleService productsOnSaleService)
        {
            _productsOnSaleService = productsOnSaleService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productsOnSaleService.GetAll();

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("GetById")]
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
        public async Task<IActionResult> Add([FromForm] ProductsOnSale productsOnSale, [FromForm] ImageUpload file,
            [FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _productsOnSaleService.Add(productsOnSale, file.Image, id, securityKey);
            
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest();
        }
        
        
        
        [HttpGet("getuserproducts")]
        public async Task<IActionResult> GetUserProducts([FromHeader] int id, [FromHeader] string securityKey)
        {
            var result = await _productsOnSaleService.GetUserProducts(id, securityKey);
        
            if (result.Success)
            {
                return Ok(result);
            }
        
            return BadRequest(result);
        }
        
        [HttpPost("deleteproduct")]
        public async Task<IActionResult> Delete([FromForm] int productId, [FromHeader] int id,
            [FromHeader] string securityKey)
        {
            var result = await _productsOnSaleService.Delete(productId, id, securityKey);
        
            if (result.Success)
            {
                return Ok(result);
            }
        
            return BadRequest(result);
        }
    }
}