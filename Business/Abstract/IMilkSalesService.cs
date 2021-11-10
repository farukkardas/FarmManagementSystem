using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IMilkSalesService
    {
        IDataResult<List<MilkSales>> GetAll();
        IDataResult<MilkSales> GetById(int id);
        IDataResult<List<MilkSalesDto>> GetMilkSales();
        IResult Add(MilkSales milkSales);
        IResult Delete(MilkSales milkSales);
        IResult Update(MilkSales milkSales);
        IDataResult<List<MilkSalesDto>> GetUserMilkSales(int id, string securityKey);
    }
}