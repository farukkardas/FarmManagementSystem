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
    public class FuelConsumptionManager : IFuelConsumptionService
    {
        private readonly IFuelConsumptionDal _fuelConsumptionDal;
        private readonly IUserDal _userDal;
        private readonly IAuthService _authService;

        public FuelConsumptionManager(IFuelConsumptionDal fuelConsumptionDal, IUserDal userDal,
            IAuthService authService)
        {
            _fuelConsumptionDal = fuelConsumptionDal;
            _userDal = userDal;
            _authService = authService;
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFuelConsumption.Get"))]
        public async Task<IDataResult<List<FuelConsumption>>> GetAll()
        {
            var result = await _fuelConsumptionDal.GetAll();

            return new SuccessDataResult<List<FuelConsumption>>(result);
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFuelConsumption.Get"))]
        public async Task<IDataResult<FuelConsumption>> GetById(int id)
        {
            var result = await _fuelConsumptionDal.Get(f => f.Id == id);

            return new SuccessDataResult<FuelConsumption>(result);
        }


        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFuelConsumption.Get"))]
        [ValidationAspect(typeof(FuelValidator))]
        public async Task<IResult> Add(FuelConsumption fuelConsumption, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<FuelConsumption>>(conditionResult.Message);
            }

            await _fuelConsumptionDal.Add(fuelConsumption);

            return new SuccessResult($"Fuel {Messages.SuccessfullyAdded}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFuelConsumption.Get"))]
        public async Task<IResult> Delete(FuelConsumption fuelConsumption, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<FuelConsumption>>(conditionResult.Message);
            }

           await _fuelConsumptionDal.Delete(fuelConsumption);
            return new SuccessResult($"Fuel {Messages.SuccessfullyDeleted}");
        }

        [SecuredOperations("admin,user")]
        [CacheRemoveAspect(("IFuelConsumption.Get"))]
        [ValidationAspect(typeof(FuelValidator))]
        public async Task<IResult> Update(FuelConsumption fuelConsumption, int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<FuelConsumption>>(conditionResult.Message);
            }

           await _fuelConsumptionDal.Update(fuelConsumption);
            return new SuccessResult($"Fuel {Messages.SuccessfullyUpdated}");
        }

        [CacheAspect(20)]
        [SecuredOperations("admin,user")]
        public async Task<IDataResult<List<FuelConsumption>>> GetUserFuelConsumptions(int id, string securityKey)
        {
            IResult conditionResult = BusinessRules.Run(await _authService.UserOwnControl(id, securityKey));

            if (conditionResult != null)
            {
                return new ErrorDataResult<List<FuelConsumption>>(conditionResult.Message);
            }

            var fuelConsumptions = await
                _fuelConsumptionDal.GetAll(c => c.OwnerId == id);

            return new SuccessDataResult<List<FuelConsumption>>(fuelConsumptions);
        }
    }
}