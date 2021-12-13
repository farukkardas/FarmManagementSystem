using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface IProductsOnSaleService
    {
        IDataResult<List<ProductsOnSale>> GetAll();
        IDataResult<ProductDetailDto> GetById(int id);
        IDataResult<List<ProductsOnSale>> GetUserProducts(int id, string securityKey);
        IResult Add(ProductsOnSale productsOnSale,IFormFile file,int id,string securityKey);
        IResult Delete(int productId,int id,string securityKey);
        IResult Update(ProductsOnSale productsOnSale,int id,string securityKey);
       
    }
}