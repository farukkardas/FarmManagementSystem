using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IProductsOnSaleService
    {
        Task<IDataResult<List<ProductsOnSale>>> GetAll();
        Task<IDataResult<ProductDetailDto>> GetById(int id);
        Task<IDataResult<List<ProductsOnSale>>> GetUserProducts(int id, string securityKey);
        Task<IResult> Add(ProductsOnSale productsOnSale,IFormFile file,int id,string securityKey);
        Task<IResult> Delete(int productId,int id,string securityKey);
        Task<IResult> Update(ProductsOnSale productsOnSale,int id,string securityKey);
       
    }
}