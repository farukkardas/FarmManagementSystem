using System.Collections.Generic;
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
    public class MilkSalesManager : IMilkSalesService
    {
        private readonly IMilkSalesDal _milkSalesDal;
        private readonly IAuthService _authService;
        public MilkSalesManager(IMilkSalesDal milkSalesDal, IAuthService authService)
        {
            _milkSalesDal = milkSalesDal;
            _authService = authService;
        }

        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public IDataResult<List<MilkSales>> GetAll()
        {
            var result = _milkSalesDal.GetAll();

            return new SuccessDataResult<List<MilkSales>>(result);
        }

        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public IDataResult<MilkSales> GetById(int id)
        {
            var result = _milkSalesDal.Get(m => m.Id == id);

            return new SuccessDataResult<MilkSales>(result);
        }

        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public IDataResult<List<MilkSalesDto>> GetMilkSales()
        {
            var result = _milkSalesDal.GetMilkSales();

            return new SuccessDataResult<List<MilkSalesDto>>(result);
        }

        [CacheRemoveAspect("IMilkSalesService.Get")]
        [SecuredOperations("admin,user")]
        [ValidationAspect(typeof(MilkSalesValidator))]
        public IResult Add(MilkSales milkSales,int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<MilkSalesDto>>(conditionResult.Message);
            }
            
            _milkSalesDal.Add(milkSales);

            return new SuccessResult($"Milk sale {Messages.SuccessfullyAdded}");
        }
       
        [SecuredOperations("admin,user")]
        [CacheRemoveAspect("IMilkSalesService.Get")]
        public IResult Delete(MilkSales milkSales,int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<MilkSalesDto>>(conditionResult.Message);
            }
            
            _milkSalesDal.Delete(milkSales);

            return new SuccessResult($"Milk sale {Messages.SuccessfullyDeleted}");
        }
        [CacheRemoveAspect("IMilkSalesService.Get")]
        [SecuredOperations("admin,user")]
        [ValidationAspect(typeof(MilkSalesValidator))]
        public IResult Update(MilkSales milkSales,int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<MilkSalesDto>>(conditionResult.Message);
            }
            
            _milkSalesDal.Update(milkSales);

            return new SuccessResult($"Milk sale {Messages.SuccessfullyUpdated}");
        }

        [CacheAspect(20)]
        [SecuredOperations("admin,user")]
        public IDataResult<List<MilkSalesDto>> GetUserMilkSales(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<MilkSalesDto>>(conditionResult.Message);
            }

            var milkSales = _milkSalesDal.GetMilkSales(c=>c.SellerId == id);
            
            return new SuccessDataResult<List<MilkSalesDto>>(milkSales);
        }
    }
}