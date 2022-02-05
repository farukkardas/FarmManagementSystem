using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstract;
using Business.BusinessAspects;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
        private readonly IAuthService _authService;

        public CustomerManager(ICustomerDal customerDal, IAuthService authService)
        {
            _customerDal = customerDal;
            _authService = authService;
        }


        [SecuredOperations("admin")]
        public async Task<IDataResult<List<Customer>>> GetAll()
        {
            var result = await _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(result);
        }

        [SecuredOperations("admin")]
        public async Task<IDataResult<Customer>> GetById(int id)
        {
            var result = await _customerDal.Get(c => c.Id == id);

            return new SuccessDataResult<Customer>(result);
        }

        [SecuredOperations("admin")]
        public async Task<IDataResult<List<MilkSalesTotalDto>>> GetCustomerSummary()
        {
            var result = await _customerDal.MilkSalesSummary();

            return new SuccessDataResult<List<MilkSalesTotalDto>>(result);
        }


        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("ICustomerService.Get"))]
        [ValidationAspect(typeof(CustomerValidator))]
        public async Task<IResult> Add(Customer customer, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Customer>>(conditionResult.Message);
            }

            await _customerDal.Add(customer);
            return new SuccessResult($"Customer {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("ICustomerService.Get"))]
        public async Task<IResult> Delete(Customer customer, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Customer>>(conditionResult.Message);
            }

            await _customerDal.Delete(customer);
            return new SuccessResult($"Customer {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("ICustomerService.Get"))]
        [ValidationAspect(typeof(CustomerValidator))]
        public async Task<IResult> Update(Customer customer, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Customer>>(conditionResult.Message);
            }

            await _customerDal.Update(customer);
            return new SuccessResult($"Customer {Messages.SuccessfullyUpdated}");
        }


        [SecuredOperations("user,admin")]
        public async Task<IDataResult<List<MilkSalesTotalDto>>> GetUserCustomers(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<MilkSalesTotalDto>>(conditionResult.Message);
            }

            var customers = await _customerDal.MilkSalesSummary(c => c.OwnerId == id);

            return new SuccessDataResult<List<MilkSalesTotalDto>>(customers);
        }
    }
}