using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        private readonly IUserDal _userDal;

        public CustomerManager(ICustomerDal customerDal, IUserDal userDal)
        {
            _customerDal = customerDal;
            _userDal = userDal;
        }


        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(result);
            
        }

        public IDataResult<Customer> GetById(int id)
        {
            var result = _customerDal.Get(c=>c.Id == id);

            return new SuccessDataResult<Customer>(result);
        }

        public IDataResult<List<MilkSalesTotalDto>> GetCustomerSummary()
        {
            var result = _customerDal.MilkSalesSummary();

            return new SuccessDataResult<List<MilkSalesTotalDto>>(result);
        }

        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult($"Customer {Messages.SuccessfullyAdded}");
        }

        public IResult Delete(Customer customer)
        {
           _customerDal.Delete(customer);
           return new SuccessResult($"Customer {Messages.SuccessfullyDeleted}");
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult($"Customer {Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<MilkSalesTotalDto>> GetUserCustomers(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<MilkSalesTotalDto>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<MilkSalesTotalDto>>("You have not permission for this.");
            }

            var customers = _customerDal.MilkSalesSummary(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<MilkSalesTotalDto>>(customers);
        }
    }
}