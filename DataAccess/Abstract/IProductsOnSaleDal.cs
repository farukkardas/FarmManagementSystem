using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IProductsOnSaleDal : IEntityRepository<ProductsOnSale>
    {
        Task<ProductDetailDto> GetProductById(Expression<Func<ProductDetailDto, bool>> filter = null);
    }
}