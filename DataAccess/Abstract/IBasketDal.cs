using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IBasketDal : IEntityRepository<ProductInBasket>
    {
        List<BasketProductDto> GetBasketProducts(Expression<Func<BasketProductDto,bool>> filter = null);
    }
}