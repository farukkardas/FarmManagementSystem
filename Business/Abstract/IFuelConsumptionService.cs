using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFuelConsumptionService
    {
        IDataResult<List<FuelConsumption>> GetAll();
        IDataResult<FuelConsumption> GetById(int id);
        
        IResult Add(FuelConsumption fuelConsumption,int id, string securityKey);
        IResult Delete(FuelConsumption fuelConsumption,int id, string securityKey);
        IResult Update(FuelConsumption fuelConsumption,int id, string securityKey);
        IDataResult<List<FuelConsumption>> GetUserFuelConsumptions(int id, string securityKey);
    }
}