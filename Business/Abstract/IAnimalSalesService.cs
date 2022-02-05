using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IAnimalSalesService
    {
        Task<IDataResult<List<AnimalSales>>> GetAll();
        Task<IDataResult<AnimalSales>> GetById(int id);
        Task<IResult> Add(AnimalSales animalSales,int id, string securityKey);
        Task<IResult> Delete(AnimalSales animalSales,int id, string securityKey);
        Task<IResult> Update(AnimalSales animalSales,int id, string securityKey);
        Task<IDataResult<List<AnimalSalesDto>>> GetUserAnimalSales(int id, string securityKey);
    }
}