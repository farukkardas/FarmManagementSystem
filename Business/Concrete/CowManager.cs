using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Business;

namespace Business.Concrete
{
    public class CowManager : ICowService
    {
        private readonly ICowDal _cowDal;
        private readonly IAuthService _authService;

        public CowManager(ICowDal cowDal, IUserDal userDal, IAuthService authService)
        {
            _cowDal = cowDal;
            _authService = authService;
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<List<Cow>>> GetAll()
        {
            var result = await _cowDal.GetAll();
            return new SuccessDataResult<List<Cow>>(result);
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<Cow>> GetById(int id)
        {
            var result = await _cowDal.Get(c => c.Id == id);
            return new SuccessDataResult<Cow>(result);
        }

        [SecuredOperations("admin")]
        [CacheAspect(20)]
        public async Task<IDataResult<Cow>> GetByCowId(int cowId)
        {
            var result = await _cowDal.Get(c => c.CowId == cowId);
            return new SuccessDataResult<Cow>(result);
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("ICowService.Get"))]
        [ValidationAspect(typeof(CowValidator))]
        public async Task<IResult> Add(Cow cow, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }

            await _cowDal.Add(cow);
            return new SuccessResult($"Cow{Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("ICowService.Get"))]
        public async Task<IResult> Delete(Cow cow, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }

            await _cowDal.Delete(cow);
            return new SuccessResult($"Cow{Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("ICowService.Get"))]
        [ValidationAspect(typeof(CowValidator))]
        public async Task<IResult> Update(Cow cow, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }

            await _cowDal.Update(cow);
            return new SuccessResult($"Cow{Messages.SuccessfullyUpdated}");
        }

        [CacheAspect(20)]
        [SecuredOperations("user,admin")]
        public async Task<IDataResult<List<Cow>>> GetUserCows(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Cow>>(conditionResult.Message);
            }

            var cows = await _cowDal.GetAll(c => c.OwnerId == id);

            return new SuccessDataResult<List<Cow>>(cows);
        }
    }
}