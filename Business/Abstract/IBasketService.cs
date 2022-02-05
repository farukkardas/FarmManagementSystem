using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IBasketService
    {
        Task<IResult> AddToBasket(ProductInBasket productInBasket,int id,string securityKey);
        Task<IResult> Delete(ProductInBasket productInBasket);
        Task<IResult> DeleteFromBasket(ProductInBasket productInBasket,int id,string securityKey);
        Task<IDataResult<List<BasketProductDto>>> GetBasketProducts(int id,string securityKey);
    }
}