using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFuelConsumptionService
    {
        IDataResult<List<FuelConsumption>> GetAll();
        IDataResult<FuelConsumption> GetById(int id);
        
        IResult Add(FuelConsumption fuelConsumption);
        IResult Delete(FuelConsumption fuelConsumption);
        IResult Update(FuelConsumption fuelConsumption);
        IDataResult<List<FuelConsumption>> GetUserFuelConsumptions(int id, string securityKey);
    }
}