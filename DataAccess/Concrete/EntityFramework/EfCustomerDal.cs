using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer,FarmManagementContext>,ICustomerDal
    {
        public List<MilkSalesTotalDto> MilkSalesSummary(Expression<Func<MilkSalesTotalDto, bool>> filter = null)
        {
            using FarmManagementContext context = new FarmManagementContext();
            
            var result = from c in context.Customers
                join u in context.Users on c.OwnerId equals u.Id
                select new MilkSalesTotalDto
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    PhoneNumber = c.PhoneNumber,
                    TotalSalesAmount = context.MilkSales.Where(x=>x.CustomerId == c.Id).Sum(sa=>sa.Amount),
                    OwnerId = u.Id
                    
                };

            return filter == null ? result.ToList() :
                result.Where(filter).ToList();
        }
    }
}