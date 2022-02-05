using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IFuelConsumptionService
    {
        Task<IDataResult<List<FuelConsumption>>> GetAll();
        Task<IDataResult<FuelConsumption>> GetById(int id);
        Task<IResult> Add(FuelConsumption fuelConsumption,int id, string securityKey);
        Task<IResult> Delete(FuelConsumption fuelConsumption,int id, string securityKey);
        Task<IResult> Update(FuelConsumption fuelConsumption,int id, string securityKey);
        Task<IDataResult<List<FuelConsumption>>> GetUserFuelConsumptions(int id, string securityKey);
    }
}