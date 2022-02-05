using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.DataAccess.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        Task<List<OrderDetailDto>> GetUserOrders(Expression<Func<OrderDetailDto, bool>> filter = null);
    }
}