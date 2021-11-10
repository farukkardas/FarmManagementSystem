using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IEntityRepository<Customer>
    {
        List<MilkSalesTotalDto> MilkSalesSummary(Expression<Func<MilkSalesTotalDto, bool>> filter = null);

    }
}