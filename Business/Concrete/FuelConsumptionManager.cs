using System.Collections.Generic;
using Business.Abstract;
using Business.Constants;
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

        public FuelConsumptionManager(IFuelConsumptionDal fuelConsumptionDal, IUserDal userDal)
        {
            _fuelConsumptionDal = fuelConsumptionDal;
            _userDal = userDal;
        }


        public IDataResult<List<FuelConsumption>> GetAll()
        {
            var result = _fuelConsumptionDal.GetAll();

            return new SuccessDataResult<List<FuelConsumption>>(result);
        }

        public IDataResult<FuelConsumption> GetById(int id)
        {
            var result = _fuelConsumptionDal.Get(f => f.Id == id);

            return new SuccessDataResult<FuelConsumption>(result);
        }


        public IResult Add(FuelConsumption fuelConsumption)
        {
            _fuelConsumptionDal.Add(fuelConsumption);

            return new SuccessResult($"Fuel {Messages.SuccessfullyAdded}");
        }

        public IResult Delete(FuelConsumption fuelConsumption)
        {
            _fuelConsumptionDal.Delete(fuelConsumption);
            return new SuccessResult($"Fuel {Messages.SuccessfullyDeleted}");

        }

        public IResult Update(FuelConsumption fuelConsumption)
        {
            _fuelConsumptionDal.Update(fuelConsumption);
            return new SuccessResult($"Fuel {Messages.SuccessfullyUpdated}");
        }

        public IDataResult<List<FuelConsumption>> GetUserFuelConsumptions(int id, string securityKey)
        {
            var user = _userDal.Get(u => u.Id == id);

            if (user == null)
            {
                return new ErrorDataResult<List<FuelConsumption>>("User not found!");
            }
            
            if (user.SecurityKey != securityKey)
            {
                return new ErrorDataResult<List<FuelConsumption>>("You have not permission for this.");
            }

            var fuelConsumptions = _fuelConsumptionDal.GetAll(c=>c.OwnerId == id);
            
            return new SuccessDataResult<List<FuelConsumption>>(fuelConsumptions);
        }
    }
}