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

namespace Business.Concrete
{
    public class SheepManager : ISheepService
    {
        private readonly ISheepDal _sheepDal;
        private readonly IAuthService _authService;
        public SheepManager(ISheepDal sheepDal, IAuthService authService)
        {
            _sheepDal = sheepDal;
            _authService = authService;
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public IDataResult<List<Sheep>> GetAll()
        {
            var result = _sheepDal.GetAll();

            return new SuccessDataResult<List<Sheep>>(result);
        }
        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public IDataResult<Sheep> GetById(int id)
        {
            var result = _sheepDal.Get(s => s.Id == id);

            return new SuccessDataResult<Sheep>(result);
        }

        [SecuredOperations("user,admin")]
        [ValidationAspect(typeof(SheepValidator))]
        [CacheRemoveAspect("ISheepService.Get")]
        public IResult Add(Sheep sheep,int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Sheep>>(conditionResult.Message);
            }
            
            _sheepDal.Add(sheep);

            return new SuccessResult($"Sheep{Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("ISheepService.Get")]
        public IResult Delete(Sheep sheep,int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Sheep>>(conditionResult.Message);
            }
            _sheepDal.Delete(sheep);

            return new SuccessResult($"Sheep{Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("user,admin")]
        [ValidationAspect(typeof(SheepValidator))]
        [CacheRemoveAspect("ISheepService.Get")]
        public IResult Update(Sheep sheep,int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Sheep>>(conditionResult.Message);
            }
            _sheepDal.Update(sheep);

            return new SuccessResult($"Sheep{Messages.SuccessfullyUpdated}");
        }

        [CacheAspect(20)]
        [SecuredOperations("user,admin")]
        public IDataResult<List<Sheep>> GetUserSheeps(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Sheep>>(conditionResult.Message);
            }
            
            var sheeps = _sheepDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Sheep>>(sheeps);
            
        }
    }
}