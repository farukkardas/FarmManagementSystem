using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace DataAccess.Abstract
{
    public interface IAnimalSalesDal : IEntityRepository<AnimalSales>
    {
        List<AnimalSalesDto> GetAnimalSales(Expression<Func<AnimalSalesDto, bool>> filter = null);

    }
}