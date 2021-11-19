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
    public class FertilizerManager : IFertilizerService
    {
        private readonly IFertilizerDal _fertilizerDal;
        private readonly IAuthService _authService;
        public FertilizerManager(IFertilizerDal fertilizerDal,IAuthService authService)
        {
            _fertilizerDal = fertilizerDal;
            _authService = authService;
        }
        [SecuredOperations("admin")]
        public IDataResult<List<Fertilizer>> GetAll()
        {
            var result = _fertilizerDal.GetAll();

            return new SuccessDataResult<List<Fertilizer>>(result);
        }
        [SecuredOperations("admin")]
        public IDataResult<Fertilizer> GetById(int id)
        {
            var result = _fertilizerDal.Get(f => f.Id == id);

            return new SuccessDataResult<Fertilizer>(result);
        }

        
        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFertilizerService.Get"))]
        [ValidationAspect(typeof(FertilizerValidator))]
        public IResult Add(Fertilizer fertilizer,int id,string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }
            _fertilizerDal.Add(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyAdded}");
        }

        [CacheRemoveAspect(("IFertilizerService.Get"))]
        public IResult Delete(Fertilizer fertilizer,int id,string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }
            _fertilizerDal.Delete(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFertilizerService.Get"))]
        [ValidationAspect(typeof(FertilizerValidator))]
        public IResult Update(Fertilizer fertilizer,int id,string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }
            _fertilizerDal.Add(fertilizer);

            return new SuccessResult($"Fertilizer {Messages.SuccessfullyUpdated}");
        }

        [SecuredOperations("admin,user")]
        [CacheAspect(20)]
        public IDataResult<List<Fertilizer>> GetUserFertilizers(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(_authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<Fertilizer>>(conditionResult.Message);
            }
            var fertilizers = _fertilizerDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<Fertilizer>>(fertilizers);
        }
    }
}