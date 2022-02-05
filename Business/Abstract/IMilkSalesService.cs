using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IMilkSalesService
    {
       Task<IDataResult<List<MilkSales>>> GetAll();
        Task<IDataResult<MilkSales>> GetById(int id);
        Task<IDataResult<List<MilkSalesDto>>> GetMilkSales();
        Task<IResult> Add(MilkSales milkSales,int id, string securityKey);
        Task<IResult> Delete(MilkSales milkSales,int id, string securityKey);
        Task<IResult> Update(MilkSales milkSales,int id, string securityKey);
        Task<IDataResult<List<MilkSalesDto>>> GetUserMilkSales(int id, string securityKey);
    }
}