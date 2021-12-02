using System.Collections.Generic;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DataTransferObjects;

namespace Business.Abstract
{
    public interface IAnimalSalesService
    {
        IDataResult<List<AnimalSales>> GetAll();
        IDataResult<AnimalSales> GetById(int id);
        IResult Add(AnimalSales animalSales,int id, string securityKey);
        IResult Delete(AnimalSales animalSales,int id, string securityKey);
        IResult Update(AnimalSales animalSales,int id, string securityKey);
        IDataResult<List<AnimalSalesDto>> GetUserAnimalSales(int id, string securityKey);
    }
}