using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IMilkSalesDal : IEntityRepository<MilkSales>
    {
        Task<List<MilkSalesDto>> GetMilkSales(Expression<Func<MilkSalesDto, bool>> filter = null);

    }
}