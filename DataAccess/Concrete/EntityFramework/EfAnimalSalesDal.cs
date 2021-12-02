using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAnimalSalesDal : EfEntityRepositoryBase<AnimalSales,FarmManagementContext>,IAnimalSalesDal
    {
        public List<AnimalSalesDto> GetAnimalSales(Expression<Func<AnimalSalesDto, bool>> filter = null)
        {
            using FarmManagementContext context = new FarmManagementContext();

            var result = from a in context.AnimalSales
                join cu in context.Customers on a.CustomerId equals cu.Id
                select new AnimalSalesDto()
                {
                    Amount = a.Amount,
                    SalePrice = a.SalePrice,
                    AnimalType = a.AnimalType,
                    BoughtDate = a.BoughtDate,
                    CustomerId = cu.Id,
                    FirstName = cu.FirstName,
                    LastName = cu.LastName,
                    SalesId = a.Id,
                    SellerId = a.SellerId
                };
            return filter == null ? result.ToList() : result.Where(filter).ToList();

        }
    }
}