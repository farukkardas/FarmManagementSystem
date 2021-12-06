using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, FarmManagementContext>, IOrderDal
    {
        public List<OrderDetailDto> GetUserOrders(Expression<Func<OrderDetailDto, bool>> filter = null)
        {
            using FarmManagementContext context = new FarmManagementContext();

            var result = from o in context.Orders
                join cu in context.Customers on o.CustomerId equals cu.Id
                join us in context.Users on o.SellerId equals us.Id
                join pr in context.Products on o.ProductType equals pr.Id
                select new OrderDetailDto()
                {
                    Id = o.Id,
                    SellerName = us.FirstName + " " + us.LastName,
                    SellerId = us.Id,
                    CustomerName = cu.FirstName + " " + cu.LastName,
                    DeliveryAddress = o.DeliveryAddress,
                    DeliveryCity = o.DeliveryCity,
                    DeliveryDistrict = o.DeliveryDistrict,
                    ProductType = o.ProductType,
                    ProductName = pr.ProductName,
                    BoughtDate = o.BoughtDate,
                    Status = o.Status
                };
            return filter == null ? result.ToList() : result.Where(filter).ToList();
        }
    }
}