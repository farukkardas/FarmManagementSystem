using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBasketDal : EfEntityRepositoryBase<ProductInBasket, FarmManagementContext>, IBasketDal
    {
        public async Task<List<BasketProductDto>> GetBasketProducts(Expression<Func<BasketProductDto, bool>> filter = null)
        {
           await using var context = new FarmManagementContext();

            var result = from b in context.ProductsInBasket
                join p in context.ProductsOnSale on b.ProductId equals p.Id
                select new BasketProductDto
                {
                    Id = b.Id,
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Description = p.Description,
                    ImagePath = p.ImagePath,
                    ProductPrice = p.Price,
                    UserId = b.UserId
                };

            return filter == null ? result.ToList() : result.Where(filter).ToList();
        }
    }
}