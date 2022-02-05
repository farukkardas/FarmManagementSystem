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
    public class EfMilkSalesDal : EfEntityRepositoryBase<MilkSales, FarmManagementContext>, IMilkSalesDal
    {
        public async Task<List<MilkSalesDto>> GetMilkSales(Expression<Func<MilkSalesDto, bool>> filter = null)
        {
           await using FarmManagementContext context = new FarmManagementContext();
            var result = from c in context.MilkSales
                join cu in context.Customers on c.CustomerId equals cu.Id
                select new MilkSalesDto()
                {
                    Amount = c.Amount,
                    CustomerId = cu.Id,
                    Price = c.SalePrice,
                    FirstName = cu.FirstName,
                    LastName = cu.LastName,
                    SalesId = c.Id,
                    BoughtDate = c.BoughtDate,
                    SellerId = c.SellerId
                };

            return filter == null ? result.ToList() : result.Where(filter).ToList();
        }

       
    }
}