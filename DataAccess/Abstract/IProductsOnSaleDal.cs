using System;
using System.Linq.Expressions;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IProductsOnSaleDal : IEntityRepository<ProductsOnSale>
    {
        ProductDetailDto GetProductById(Expression<Func<ProductDetailDto, bool>> filter = null);
    }
}