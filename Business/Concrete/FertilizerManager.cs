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
    public class FertilizerManager : IFertilizerService
    {
        private readonly IFertilizerDal _fertilizerDal;
        private readonly IAuthService _authService;

        public FertilizerManager(IFertilizerDal fertilizerDal, IAuthService authService)
        {
            _fertilizerDal = fertilizerDal;
            _authService = authService;
        }

        [SecuredOperations("admin")]
        public async Task<IDataResult<List<Fertilizer>>> GetAll()
        {
            var result = await _fertilizerDal.GetAll();

            return new SuccessDataResult<List<Fertilizer>>(result);
        }

        [SecuredOperations("admin")]
        public async Task<IDataResult<Fertilizer>> GetById(int id)
        {
            var result = await _fertilizerDal.Get(f => f.Id == id);

            return new SuccessDataResult<Fertilizer>(result);
        }


        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFertilizerService.Get"))]
        [ValidationAspect(typeof(FertilizerValidator))]
        public async Task<IResult> Add(Fertilizer fertilizer, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }

            await _fertilizerDal.Add(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyAdded}");
        }

        [CacheRemoveAspect(("IFertilizerService.Get"))]
        public async Task<IResult> Delete(Fertilizer fertilizer, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }

            await _fertilizerDal.Delete(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFertilizerService.Get"))]
        [ValidationAspect(typeof(FertilizerValidator))]
        public async Task<IResult> Update(Fertilizer fertilizer, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }

            await _fertilizerDal.Add(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("admin,user")]
        [CacheAspect(20)]
        public async Task<IDataResult<List<Fertilizer>>> GetUserFertilizers(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }

            var fertilizers = await _fertilizerDal.GetAll(c => c.OwnerId == id);

            return new SuccessDataResult<List<Fertilizer>>(fertilizers);
        }
    }
}