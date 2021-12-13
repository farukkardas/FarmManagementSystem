using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IBasketService
    {
        IResult AddToBasket(ProductInBasket productInBasket,int id,string securityKey);
        IResult DeleteFromBasket(ProductInBasket productInBasket,int id,string securityKey);
        IDataResult<List<BasketProductDto>> GetBasketProducts(int id,string securityKey);
    }
}