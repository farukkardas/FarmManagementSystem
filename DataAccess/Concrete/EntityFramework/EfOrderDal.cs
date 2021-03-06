using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, FarmManagementContext>, IOrderDal
    {
        public async Task<List<OrderDetailDto>> GetUserOrders(Expression<Func<OrderDetailDto, bool>> filter = null)
        {
            await using FarmManagementContext context = new FarmManagementContext();

            var result = from o in context.Orders
                join u in context.Users on o.CustomerId equals u.Id
                join us in context.Users on o.SellerId equals us.Id
                join pr in context.Products on o.ProductType equals pr.Id
                join pOnSale in context.ProductsOnSale on o.ProductId equals pOnSale.Id
                select new OrderDetailDto
                {
                    Id = o.Id,
                    SellerName = us.FirstName + " " + us.LastName,
                    CustomerId = o.CustomerId,
                    SellerId = us.Id,
                    ProductId = pOnSale.Id,
                    Price = o.Price,
                    CustomerName = u.FirstName + " " + u.LastName,
                    DeliveryAddress = o.DeliveryAddress,
                    DeliveryCity = o.DeliveryCity,
                    DeliveryDistrict = o.DeliveryDistrict,
                    ProductType = o.ProductType,
                    ImagePath = pOnSale.ImagePath,
                    ProductName = pOnSale.Name,
                    BoughtDate = o.BoughtDate,
                    Status = o.Status,
                    DeliveryNo = o.DeliveryNo
                    
                };
            return filter == null ? result.ToList() : result.Where(filter).ToList();
        }
    }
}