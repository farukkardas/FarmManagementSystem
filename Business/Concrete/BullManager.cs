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
    public class BullManager : IBullService
    {
        private readonly IBullDal _bullDal;
        private readonly IAuthService _authService;
        
        public BullManager(IBullDal bullDal,IAuthService authService)
        {
            _bullDal = bullDal;
           
            _authService = authService;
        }
     
        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public IDataResult<List<Bull>> GetAll()
        {
            var result = _bullDal.GetAll();
            return new SuccessDataResult<List<Bull>>(result);
        }
        
        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public IDataResult<Bull> GetById(int id)
        {
            var result = _bullDal.Get(b => b.Id == id);
            return new SuccessDataResult<Bull>(result);
        }
        
        [SecuredOperations("user,admin")]
        [ValidationAspect(typeof(BullValidator))]
        [CacheRemoveAspect("IBullService.Get")]
        public IResult Add(Bull bull,int id,string securityKey)
        {
             IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }
            
            _bullDal.Add(bull);
            return new SuccessResult($"Bull{Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IBullService.Get")]
        public IResult Delete(Bull bull,int id,string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }
            _bullDal.Delete(bull);
            return new SuccessResult($"Bull{Messages.SuccessfullyDeleted}");
        }

        [ValidationAspect(typeof(BullValidator))]
        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IBullService.Get")]
        public IResult Update(Bull bull,int id,string securityKey)
        {
          
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }
            
            _bullDal.Update(bull);
            return new SuccessResult($"Bull{Messages.SuccessfullyUpdated}");
        }

        [CacheAspect(20)]
        [SecuredOperations("user,admin")]
        public IDataResult<List<Bull>> GetUserBulls(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }

            var bulls = _bullDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Bull>>(bulls);
            
        }
    }
}