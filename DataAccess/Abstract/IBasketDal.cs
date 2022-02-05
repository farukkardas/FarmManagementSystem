using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IBasketDal : IEntityRepository<ProductInBasket>
    {
        Task<List<BasketProductDto>> GetBasketProducts(Expression<Func<BasketProductDto,bool>> filter = null);
    }
}