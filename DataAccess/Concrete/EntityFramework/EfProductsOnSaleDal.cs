using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductsOnSaleDal : EfEntityRepositoryBase<ProductsOnSale, FarmManagementContext>, IProductsOnSaleDal
    {
        public async Task<ProductDetailDto> GetProductById(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            await using var context = new FarmManagementContext();

            var result = from p in context.ProductsOnSale
                join u in context.Users on p.SellerId equals u.Id
                select new ProductDetailDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    EntryDate = p.EntryDate,
                    ImagePath = p.ImagePath,
                    PhoneNumber = u.PhoneNumber,
                    SellerId = p.SellerId,
                    SellerName = u.FirstName,
                    SellerLastName = u.LastName
                };
            return filter == null ? result.FirstOrDefault() : result.Where(filter).FirstOrDefault();
        }
    }
}