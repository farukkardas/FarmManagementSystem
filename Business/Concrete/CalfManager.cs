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
    public class CalfManager : ICalfService
    {
        private readonly ICalfDal _calfDal;
        private readonly IAuthService _authService;

        public CalfManager(ICalfDal calfDal, IAuthService authService)
        {
            _calfDal = calfDal;
            _authService = authService;
        }

        [CacheAspect(20)]
        [SecuredOperations("user,admin")]
        public async Task<IDataResult<List<Calves>>> GetAll()
        {
            var result = await _calfDal.GetAll();
            return new SuccessDataResult<List<Calves>>(result);
        }

        [CacheAspect(20)]
        [SecuredOperations("user,admin")]
        public async Task<IDataResult<Calves>> GetById(int id)
        {
            var result = await _calfDal.Get(c => c.Id == id);
            return new SuccessDataResult<Calves>(result);
        }

        [ValidationAspect(typeof(CalfValidator))]
        [CacheRemoveAspect("ICalfService.Get")]
        public async Task<IResult> Add(Calves calves, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }

            await _calfDal.Add(calves);
            return new SuccessResult($"Calf{Messages.SuccessfullyAdded}");
        }

        [CacheRemoveAspect("ICalfService.Get")]
        public async Task<IResult> Delete(Calves calves, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }

            await _calfDal.Delete(calves);
            return new SuccessResult($"Calf{Messages.SuccessfullyDeleted}");
        }

        [ValidationAspect(typeof(CalfValidator))]
        [CacheRemoveAspect("ICalfService.Get")]
        public async Task<IResult> Update(Calves calves, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Bull>>(conditionResult.Message);
            }

            await _calfDal.Update(calves);
            return new SuccessResult($"Calf{Messages.SuccessfullyUpdated}");
        }

        [CacheAspect(20)]
        [SecuredOperations("user,admin")]
        public async Task<IDataResult<List<Calves>>> GetUserCalves(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Calves>>(conditionResult.Message);
            }

            var calves = await _calfDal.GetAll(c => c.OwnerId == id);

            return new SuccessDataResult<List<Calves>>(calves);
        }
    }
}