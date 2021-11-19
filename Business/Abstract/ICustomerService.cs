using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IDataResult<Customer> GetById(int id);
        IDataResult<List<MilkSalesTotalDto>> GetCustomerSummary();
        IResult Add(Customer customer,int id, string securityKey);
        IResult Delete(Customer customer,int id, string securityKey);
        IResult Update(Customer customer,int id, string securityKey);
        IDataResult<List<MilkSalesTotalDto>> GetUserCustomers(int id, string securityKey);
    }
}