using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IEntityRepository<Customer>
    {
        Task<List<MilkSalesTotalDto>> MilkSalesSummary(Expression<Func<MilkSalesTotalDto, bool>> filter = null);

    }
}