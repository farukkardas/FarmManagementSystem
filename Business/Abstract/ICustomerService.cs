using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<List<Customer>>> GetAll();
        Task<IDataResult<Customer>> GetById(int id);
        Task<IDataResult<List<MilkSalesTotalDto>>> GetCustomerSummary();
        Task<IResult> Add(Customer customer,int id, string securityKey);
        Task<IResult> Delete(Customer customer,int id, string securityKey);
        Task<IResult> Update(Customer customer,int id, string securityKey);
        Task<IDataResult<List<MilkSalesTotalDto>>> GetUserCustomers(int id, string securityKey);
    }
}