using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductsOnSaleDal : EfEntityRepositoryBase<ProductsOnSale,FarmManagementContext>, IProductsOnSaleDal
    {
        
    }
}