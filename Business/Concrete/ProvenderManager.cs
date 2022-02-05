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

namespace Business.Concrete
{
    public class ProvenderManager : IProvenderService
    {
        private readonly IProvenderDal _provenderDal;
        private readonly IAuthService _authService;

        public ProvenderManager(IProvenderDal provenderDal, IAuthService authService)
        {
            _provenderDal = provenderDal;
            _authService = authService;
        }

        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public async Task<IDataResult<List<Provender>>> GetAll()
        {
            var result = await _provenderDal.GetAll();

            return new SuccessDataResult<List<Provender>>(result);
        }

        [CacheAspect(20)]
        [SecuredOperations("admin")]
        public async Task<IDataResult<Provender>> GetById(int id)
        {
            var result = await _provenderDal.Get(s => s.Id == id);

            return new SuccessDataResult<Provender>(result);
        }

        [SecuredOperations("user,admin")]
        [ValidationAspect(typeof(ProvenderValidator))]
        [CacheRemoveAspect("IProvenderService.Get")]
        public async Task<IResult> Add(Provender provender, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Provender>>(conditionResult.Message);
            }

            await _provenderDal.Add(provender);

            return new SuccessResult($"Provender {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("user,admin")]
        [CacheRemoveAspect("IProvenderService.Get")]
        public async Task<IResult> Delete(Provender provender, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Provender>>(conditionResult.Message);
            }

            await _provenderDal.Delete(provender);
            return new SuccessResult($"Provender {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("user,admin")]
        [ValidationAspect(typeof(ProvenderValidator))]
        [CacheRemoveAspect("IProvenderService.Get")]
        public async Task<IResult> Update(Provender provender, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Provender>>(conditionResult.Message);
            }

            await _provenderDal.Update(provender);
            return new SuccessResult($"Provender {Messages.SuccessfullyUpdated}");
        }

        [CacheAspect(20)]
        [SecuredOperations("user,admin")]
        public async Task<IDataResult<List<Provender>>> GetUserProvenders(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Provender>>(conditionResult.Message);
            }

            await _authService.UserOwnControl(id, securityKey);
            var provenders = await _provenderDal.GetAll(c => c.OwnerId == id);

            return new SuccessDataResult<List<Provender>>(provenders);
        }
    }
}