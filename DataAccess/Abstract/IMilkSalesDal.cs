using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IMilkSalesDal : IEntityRepository<MilkSales>
    {
        List<MilkSalesDto> GetMilkSales(Expression<Func<MilkSalesDto, bool>> filter = null);

    }
}